using S.S.L.Domain.Enums;
using S.S.L.Domain.Interfaces.Repositories;
using S.S.L.Domain.Interfaces.Utilities;
using S.S.L.Domain.Models;
using System;
using System.Collections.Generic;
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

        public async Task<List<UserModel>> GetUsersAsync(UserType type)
        {
            return await _repo.GetUsers(type);
        }



        /// <summary>
        /// Registers new users on the platform
        /// </summary>
        /// <param name="newUser"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<UserModel> RegisterAsync(UserModel newUser, string password)
        {

            //check if email already exists
            var user = await _repo.VerifyUserAsync(newUser.Email);


            //hash password
            var passHash = _encryption.Encrypt(password);

            //save user
            return await _repo.AddUserAsync(newUser, passHash);

        }

        public async Task<UserModel> RegisterFacilitator(UserModel newMentor, bool makeAdmin)
        {
            var passHash = _encryption.Encrypt("password");

            return await _repo.AddFacilitator(newMentor, makeAdmin, passHash);
        }

        public async Task<UserModel> GetUserById(int userId)
        {
            return await _repo.GetUserAsync(userId);
        }

        public async Task UpdateProfile(int userId, UserModel model)
        {
            await _repo.UpdateUserProfile(userId, model);
        }

        public async Task<UserModel> ConfirmUser(int userId)
        {
            return await _repo.ConfirmUser(userId);
        }

        public async Task<UserModel> ValidateEmail(string email)
        {
            var user = await _repo.VerifyUserAsync(email);
            if (user == null)
                throw new Exception("Sorry. We don't know you.");

            return user;
        }

        public async Task RecoverPasswordAsync(string email, string password)
        {
            //hash password
            var passwordHash = _encryption.Encrypt(password);
            await _repo.ResetPassword(email, passwordHash);
        }
        public async Task RemoveUser(int userId)
        {
            await _repo.RemoveUser(userId);
        }

        public async Task UpdateUserRole(int userId)
        {
            await _repo.UpdateUserRole(userId);
        }
    }
}
