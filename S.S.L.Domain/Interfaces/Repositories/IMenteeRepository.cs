using S.S.L.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace S.S.L.Domain.Interfaces.Repositories
{
    public interface IMenteeRepository
    {
        Task AddMentee(int userId);
        Task<List<UserModel>> GetMentees(bool mentored);
    }
}
