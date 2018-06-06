using S.S.L.Domain.Interfaces.Repositories;
using S.S.L.Domain.Models;
using S.S.L.Infrastructure.S.S.L.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
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
                Email = model.Email,
                MobileNumber = model.MobileNumber,
                PasswordHash = passwordHash,
                EmailConfirmed = false,
                Gender = model.Gender,
                RoleId = 2
                //Add other fields
            };

            _context.Users.Add(newUser);
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
                            .Where(u => u.Email == email && u.PasswordHash == passwordHash)
                            .FirstOrDefaultAsync();

            if (user == null)
                throw new Exception("User does not exist");

            return UserFormatter(user);


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
        /// <param name="query"></param>
        /// <returns></returns>
        private static UserModel UserFormatter(User query)
        {
            return new UserModel
            {
                Id = query.Id,
                FirstName = query.FirstName,
                LastName = query.LastName,
                Email = query.Email,

            };
        }

        
    }
}
