using S.S.L.Domain.Interfaces.Repositories;
using S.S.L.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace S.S.L.Domain.Managers
{
    public class CustomManager
    {
        private readonly ICustomRepository _repo;
        private readonly IStateCountryRepository _locationRepo;

        public CustomManager(ICustomRepository repo, IStateCountryRepository locationRepo)
        {
            _repo = repo;
            _locationRepo = locationRepo;
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

        public async Task<List<StateModel>> GetStates(int? countryId)
        {
            var states = await _locationRepo.GetStatesAsync(countryId);
            return states;
        }

        public async Task<List<CountryModel>> GetCountries()
        {
            var countries = await _locationRepo.GetCountriesAsync();
            return countries;

        }
    }
}
