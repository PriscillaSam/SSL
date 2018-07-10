using S.S.L.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace S.S.L.Domain.Interfaces.Repositories
{
    public interface IMenteeRepository
    {
        Task AddMentee(int userId);
        Task<List<UserModel>> GetMentees(bool mentored);
        Task AssignOrUpdateFacilitator(int menteeId, int facilitatorId);
        Task<UserModel> GetFacilitator(int userId);
        Task<UserModel> GetMentee(int userId);
    }
}
