using S.S.L.Domain.Interfaces.Repositories;
using S.S.L.Domain.Interfaces.Utilities;
using S.S.L.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S.S.L.Domain.Managers
{
    public class UserManager
    {
        private readonly IUserRepository _repo;
        private readonly IEncryption _encryption;

        public UserManager(IUserRepository repo, IEncryption encryption)
        {
            _repo = repo;
            _encryption = encryption;
        }

        /// <summary>
        /// Provides backend user login functionality
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>UserModel</returns>
        public async Task<UserModel> Login(string email, string password)
        {

            //Encrypt password string
            var passHash = _encryption.Encrypt(password);
            return await _repo.ValidateUserAsync(email, passHash);            
        } 


       /// <summary>
       /// Registers new users on the platform
       /// </summary>
       /// <param name="newUser"></param>
       /// <param name="password"></param>
       /// <returns></returns>
        public async Task<UserModel> Register(UserModel newUser, string password)
        {

            //check if email already exists
            var emailExists = await _repo.VerifyEmailAsync(newUser.Email);
            if (emailExists)
                throw new Exception("This email is already in use");

            //hash password
            var passHash = _encryption.Encrypt(password);

            //save user
            return await _repo.AddUserAsync(newUser, passHash);

        }
    }
}
