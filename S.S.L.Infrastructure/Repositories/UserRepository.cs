﻿using S.S.L.Domain.Enums;
using S.S.L.Domain.Interfaces.Repositories;
using S.S.L.Domain.Models;
using S.S.L.Infrastructure.S.S.L.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace S.S.L.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private Entities _context;

        public UserRepository(Entities context)
        {
            _context = context;
        }

        /// <summary>
        /// Registers a new user 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        public async Task<UserModel> AddUserAsync(UserModel model, string passwordHash)
        {

            //transform  to user model
            var newUser = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email.ToLower(),
                EmploymentStatus = model.EmploymentStatus,
                MobileNumber = model.MobileNumber,
                PasswordHash = passwordHash,
                EmailConfirmed = false,
                Gender = model.Gender,
                Country = model.Country,
                State = model.State,
                UserType = model.UserType,

                //Add other fields
            };

            _context.Users.Add(newUser);
            AddUserRole(newUser, UserType.Mentee);
            await _context.SaveChangesAsync();

            model.Id = newUser.Id;

            return model;
        }


        /// <summary>
        /// Checks if user with matching email and password exists
        /// </summary>
        /// <param name="email"></param>
        /// <param name="passwordHash"></param>
        /// <returns>Task<UserModel></returns>
        public async Task<UserModel> ValidateUserAsync(string email, string passwordHash)
        {
            var user = await _context
                            .Users
                            .Where(u => u.Email == email.ToLower() && u.PasswordHash == passwordHash)
                            .FirstOrDefaultAsync();

            if (user == null)
                throw new Exception("Sorry, Email or Password is invalid");
            if (user.IsDeleted)
                throw new Exception("Sorry, you are unauthorized to be here");
            if (!user.EmailConfirmed)
                throw new Exception("Please confirm your email before proceeding");

            var _user = UserFormatter(user);
            _user.Roles = GetUserRoles(user.Id);

            return _user;
        }

        public IEnumerable<string> GetUserRoles(int userId)
        {
            var roles = from ur in _context.UserRoles
                        where ur.UserId == userId
                        from r in _context.Roles
                        where ur.RoleId == r.Id
                        select r.Name;

            return roles.ToList();

        }

        public async Task<UserModel> VerifyUserAsync(string email)
        {
            var user = await _context
                              .Users
                              .FirstOrDefaultAsync(u => u.Email == email.ToLower() && !u.IsDeleted);

            if (user == null) return null;

            return UserFormatter(user);
        }


        private async Task<User> GetUser(int userId)
        {
            return await _context.Users
                .Where(u => u.Id == userId && !u.IsDeleted)
                .FirstOrDefaultAsync();
        }

        public async Task<UserModel> GetUserAsync(int userId)
        {
            var user = await GetUser(userId);
            if (user == null) return null;

            return new UserModel
            {

                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Country = user.Country,
                State = user.State,
                Gender = user.Gender,
                MobileNumber = user.MobileNumber,
                UserType = user.UserType

            };
        }
        public async Task UpdateUserProfile(int userId, UserModel model)
        {
            var existingUser = await GetUser(userId);
            if (existingUser == null) return;

            existingUser.FirstName = model.FirstName;
            existingUser.LastName = model.LastName;
            existingUser.Country = model.Country;
            existingUser.State = model.State;
            existingUser.MobileNumber = model.MobileNumber;

            _context.Entry(existingUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task<UserModel> ConfirmUser(int userId)
        {

            var user = await GetUser(userId);
            if (user == null || user.EmailConfirmed)
                throw new Exception("Sorry, this operation is not valid.");

            user.EmailConfirmed = true;
            _context.Entry(user).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return UserFormatter(user);

        }

        public async Task ResetPassword(string email, string passwordHash)
        {
            var user = await _context.Users
                .Where(u => u.Email == email.ToLower() && !u.IsDeleted)
                .FirstOrDefaultAsync();

            if (user == null) throw new Exception("Sorry, we don't know you.");

            user.PasswordHash = passwordHash;
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }

        /// <summary>
        /// Get all the users of a particular usertype
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<List<UserModel>> GetUsers(UserType type)
        {

            var users = await _context.Users
                .Where(u => u.UserType == type)
                .Where(u => !u.IsDeleted)
                .Select(u => new UserModel
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    MobileNumber = u.MobileNumber,
                    Gender = u.Gender,
                    GymGroup = u.GymGroup,
                    Id = u.Id,

                }).ToListAsync();
            users.ForEach(u =>
            {
                u.Roles = GetUserRoles(u.Id);
            });

            return users;
        }

        public async Task UpdateUserRole(int userId)
        {
            var user = await GetUser(userId);


            if (user.UserType == UserType.Mentee)
            {

                //get previous mentee role
                var menteeRole = await _context.UserRoles
                    .Where(u => u.UserId == user.Id)
                    .FirstOrDefaultAsync();

                //remove mentee role
                _context.UserRoles.Remove(menteeRole);

                //add facilitator model
                var facilitator = new Facilitator
                {
                    UserId = user.Id
                };
                user.UserType = UserType.Facilitator;
                _context.Facilitators.Add(facilitator);
                AddUserRole(user, UserType.Facilitator);
            }

            else if (user.UserType == UserType.Facilitator)
            {
                AddUserRole(user, UserType.Administrator);
            }

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Registers a user as a facilitator or mentor
        /// </summary>
        /// <param name="newFacilitator"></param>
        /// <param name="makeAdmin"></param>
        /// <returns></returns>
        public async Task<UserModel> AddFacilitator(UserModel newFacilitator, bool makeAdmin, string passHash)
        {

            var existingUser = await _context.Users.AnyAsync(u => u.Email == newFacilitator.Email);

            if (existingUser)
                throw new Exception("Sorry, this email is already taken.");

            var user = new User
            {
                FirstName = newFacilitator.FirstName,
                LastName = newFacilitator.LastName,
                Email = newFacilitator.Email,
                PasswordHash = passHash,
                Gender = newFacilitator.Gender,
                UserType = UserType.Facilitator
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            //add facilitator model
            var facilitator = new Facilitator
            {
                UserId = user.Id
            };

            _context.Facilitators.Add(facilitator);

            AddUserRole(user, UserType.Facilitator);
            if (makeAdmin)
                AddUserRole(user, UserType.Administrator);

            await _context.SaveChangesAsync();
            return UserFormatter(user);
        }

        public async Task<List<GymGroupView>> GetGymGroupingsAsync()
        {
            var groups = await _context.Users
                .Where(u => u.GymGroup != 0 && !u.IsDeleted)
                .GroupBy(u => u.GymGroup)
                .Select(g => new GymGroupView
                {
                    GroupName = g.Key,
                    GroupMembers = g.Select(u => new UserModel
                    {
                        Id = u.Id,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        UserType = u.UserType,
                        Gender = u.Gender
                    }).ToList()

                }).ToListAsync();

            return groups;
        }

        public async Task AssignGymGroup(int userId, GymGroup group)
        {
            var user = await GetUser(userId);
            if (user == null) throw new Exception("User does not exist");

            user.GymGroup = group;
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task RemoveUserFromGym(int userId)
        {
            var user = await GetUser(userId);
            if (user == null) throw new Exception("User does not exist");
            user.GymGroup = 0;
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<List<UserModel>> GetGymMembers(int userId)
        {
            var user = await GetUser(userId);
            var users = await _context.Users
                .Where(u => u.GymGroup == user.GymGroup && !u.IsDeleted)
                .Select(u => new UserModel
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    MobileNumber = u.MobileNumber

                }).ToListAsync();

            return users;
        }

        public async Task RemoveUser(int userId)
        {
            var user = await GetUser(userId);
            if (user == null) throw new Exception("This user does not exist");

            user.IsDeleted = true;

            if (user.UserType == UserType.Facilitator)
            {

                var facilitator = await _context.Facilitators
                    .Where(f => f.UserId == user.Id)
                    .FirstOrDefaultAsync();

                await _context.Mentees
                   .Where(m => m.FacilitatorId == facilitator.Id)
                   .ForEachAsync(m =>
                   {
                       m.FacilitatorId = null;

                   });
            }

            if (user.UserType == UserType.Mentee)
            {
                var mentee = await _context.Mentees.FirstOrDefaultAsync(u => u.UserId == user.Id);
                mentee.FacilitatorId = null;

            }

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<List<UserModel>> GetRemovedUsers()
        {

            var removed = await _context.Users
                           .Where(u => u.IsDeleted)
                           .Select(user => new UserModel
                           {
                               Id = user.Id,
                               FirstName = user.FirstName,
                               LastName = user.LastName,
                               Email = user.Email,
                               UserType = user.UserType
                           })
                           .ToListAsync();

            return removed;

        }

        public async Task RestoreUser(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            user.IsDeleted = false;

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Custom formatter for transforming User Object to UserModel Object
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private static UserModel UserFormatter(User user)
        {
            return new UserModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserType = user.UserType

            };
        }

        private void AddUserRole(User user, UserType type)
        {
            var role = _context.Roles
                .Where(r => r.Name == type.ToString())
                .FirstOrDefault();

            var userRole = new UserRole(role, user);

            user.UserRoles.Add(userRole);
            _context.UserRoles.Add(userRole);
        }


    }
}
