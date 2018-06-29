using S.S.L.Domain.Enums;
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
                              .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null) return null;

            return UserFormatter(user);
        }



        public async Task<UserModel> GetUserAsync(int userId)
        {
            var user = await _context.Users.Where(u => u.Id == userId).SingleOrDefaultAsync();
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

        public async Task<UserModel> ConfirmUser(int userId)
        {
            var user = await _context.Users.Where(u => u.Id == userId).SingleOrDefaultAsync();
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
                .Where(u => u.Email == email)
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
                .Select(u => new UserModel
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    MobileNumber = u.MobileNumber,
                    Gender = u.Gender

                }).ToListAsync();

            return users;
        }

        /// <summary>
        /// Registers a user as a facilitator or mentor
        /// </summary>
        /// <param name="newMentor"></param>
        /// <param name="makeAdmin"></param>
        /// <returns></returns>
        public async Task AddFacilitator(UserModel newMentor, bool makeAdmin)
        {

            var existingUser = await _context.Users.AnyAsync(u => u.Email == newMentor.Email);
            if (existingUser)
                throw new Exception("Sorry, this email is already taken.");

            var user = new User
            {
                FirstName = newMentor.FirstName,
                LastName = newMentor.LastName,
                Email = newMentor.Email,
                UserType = UserType.Facilitator
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            AddUserRole(user, UserType.Facilitator);
            if (makeAdmin)
                AddUserRole(user, UserType.Administrator);

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
            var userRole = new UserRole
            {
                User = user,
                Role = _context.Roles.Where(r => r.Name == type.ToString()).FirstOrDefault()
            };

            user.UserRoles.Add(userRole);
        }
    }
}
