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
            await _context.SaveChangesAsync();

            model.Id = newUser.Id;

            var userRole = new UserRole
            {
                UserId = model.Id,
                RoleId = _context.Roles.Where(r => r.Name == "Mentee").FirstOrDefault().Id
            };

            _context.UserRoles.Add(userRole);
            await _context.SaveChangesAsync();

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
                throw new Exception("Invalid Email or password");

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

        public async Task<bool> VerifyEmailAsync(string email)
        {

            var emailTaken = await _context
                              .Users
                              .AnyAsync(u => u.Email == email);

            return emailTaken;
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
            };
        }

        public async Task<UserModel> GetUserAsync(int userId)
        {
            var user = await _context.Users.Where(u => u.Id == userId).SingleOrDefaultAsync();
            if (user == null) return null;
            return UserFormatter(user);
        }

        public async Task<UserModel> ConfirmUser(int userId)
        {
            var user = await _context.Users.Where(u => u.Id == userId).SingleOrDefaultAsync();
            if (user == null || user.EmailConfirmed) throw new Exception("This operation is not valid.");

            user.EmailConfirmed = true;
            await _context.SaveChangesAsync();

            return UserFormatter(user);

        }
    }
}
