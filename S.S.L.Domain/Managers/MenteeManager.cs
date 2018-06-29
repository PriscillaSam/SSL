using S.S.L.Domain.Interfaces.Repositories;
using System.Threading.Tasks;

namespace S.S.L.Domain.Managers
{
    public class MenteeManager
    {
        private readonly IMenteeRepository _repo;

        public MenteeManager(IMenteeRepository repo)
        {
            _repo = repo;
        }

        public async Task CreateMentee(int userId)
        {
            await _repo.AddMentee(userId);
        }
    }
}
