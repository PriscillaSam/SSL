using S.S.L.Domain.Interfaces.Repositories;

namespace S.S.L.Domain.Managers
{
    public class ForumManager
    {
        private readonly IForumRepository _repo;

        public ForumManager(IForumRepository repo)
        {
            _repo = repo;
        }
    }
}
