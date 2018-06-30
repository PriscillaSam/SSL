using S.S.L.Domain.Enums;
using S.S.L.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace S.S.L.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<UserModel> ValidateUserAsync(string email, string password);
        Task<UserModel> VerifyUserAsync(string email);
        Task<UserModel> AddUserAsync(UserModel model, string passwordHash);
        Task<UserModel> GetUserAsync(int userId);
        Task<UserModel> ConfirmUser(int userId);
        Task ResetPassword(string email, string password);
        Task<List<UserModel>> GetUsers(UserType type);
        Task AddFacilitator(UserModel newMentor, bool makeAdmin, string passHash);
    }
}
