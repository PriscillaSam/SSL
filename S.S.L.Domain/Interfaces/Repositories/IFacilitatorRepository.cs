using S.S.L.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace S.S.L.Domain.Interfaces.Repositories
{
    public interface IFacilitatorRepository
    {
        Task<List<UserModel>> GetMenteesByFacilitator(int userId);
        Task<List<UserModel>> GetFacilitators(string gender);
    }
}
