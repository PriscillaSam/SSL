using S.S.L.Domain.Interfaces.Repositories;
using S.S.L.Domain.Models;
using S.S.L.Infrastructure.S.S.L.Entities;
using System;
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
                            .Where(u => u.Email == email.ToLower() && u.PasswordHash == passwordHash)
                            .FirstOrDefaultAsync();

            if (!user.EmailConfirmed)
                throw new Exception("Please confirm your email before proceeding");
            if (user == null)
                throw new Exception("Invalid Email or password");

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

        public async Task<UserModel> GetUserAsync(int userId)
        {
            var query = await _context.Users.Where(u => u.Id == userId).SingleOrDefaultAsync();
            if (query == null) return null;
            return UserFormatter(query);
        }

        public async Task<UserModel> ConfirmUser(int userId)
        {
            var query = await _context.Users.Where(u => u.Id == userId).SingleOrDefaultAsync();
            if (query == null || query.EmailConfirmed) throw new Exception("This operation is not valid.");

            query.EmailConfirmed = true;
            await _context.SaveChangesAsync();

            return UserFormatter(query);

        }
    }
}
