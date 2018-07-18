using S.S.L.Domain.Interfaces.Repositories;
using S.S.L.Domain.Models;
using S.S.L.Infrastructure.S.S.L.Entities;
using System.Threading.Tasks;

namespace S.S.L.Infrastructure.Repositories
{
    public class ForumRepository : IForumRepository
    {
        private readonly Entities _context;

        public ForumRepository(Entities context)
        {
            _context = context;
        }


        public async Task AddForum(ForumModel model)
        {

        }
    }
}
