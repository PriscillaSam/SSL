using System.Threading.Tasks;

namespace S.S.L.Domain.Interfaces.Repositories
{
    public interface IMenteeRepository
    {
        Task AddMentee(int userId);

    }
}
