using S.S.L.Domain.Interfaces.Repositories;
using S.S.L.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace S.S.L.Domain.Managers
{
    public class FacilitatorManager
    {
        private readonly IFacilitatorRepository _repo;

        public FacilitatorManager(IFacilitatorRepository repo)
        {
            _repo = repo;
        }


        public async Task<List<MenteeUserModel>> GetFacilitatorMentees(int userId)
        {
            return await _repo.GetMenteesByFacilitator(userId);
        }

        public async Task<List<UserModel>> GetFacilitators(string gender)
        {
            return await _repo.GetFacilitators(gender);
        }


        public async Task UpdateMenteeProgress(int menteeId, int facilitatorId)
        {
            await _repo.UpdateMenteeProgressAsync(menteeId, facilitatorId);
        }
    }
}
