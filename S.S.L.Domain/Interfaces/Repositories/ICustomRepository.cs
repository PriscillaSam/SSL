using S.S.L.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace S.S.L.Domain.Interfaces.Repositories
{
    public interface ICustomRepository
    {
        Task<TodoModel> AddTodoAsync(TodoModel todo, int userId);
        Task<List<TodoModel>> GetTodosAsync(int userId);
        Task<TodoModel> UpdateTodoAsync(int todoId, int userId);
    }
}
