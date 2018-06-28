using S.S.L.Domain.Interfaces.Repositories;
using S.S.L.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace S.S.L.Domain.Managers
{
    public class CustomManager
    {
        private readonly ICustomRepository _repo;

        public CustomManager(ICustomRepository repo)
        {
            _repo = repo;
        }


        public async Task<TodoModel> CreateTodo(TodoModel model, int userId)
        {
            return await _repo.AddTodoAsync(model, userId);
        }

        public async Task<List<TodoModel>> GetUserTodos(int userId)
        {
            return await _repo.GetTodosAsync(userId);
        }

        public async Task<TodoModel> UpdateTodo(int todoId, int userId)
        {
            return await _repo.UpdateTodoAsync(todoId, userId);
        }
    }
}
