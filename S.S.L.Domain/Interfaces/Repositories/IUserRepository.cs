using S.S.L.Domain.Models;
using System.Threading.Tasks;

namespace S.S.L.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<UserModel> ValidateUserAsync(string email, string password);
        Task<bool> VerifyEmailAsync(string email);
        Task<UserModel> AddUserAsync(UserModel model, string passwordHash);
        Task<UserModel> GetUserAsync(int userId);
        Task<UserModel> ConfirmUser(int userId);

    }
}
