using S.S.L.Domain.Interfaces.Repositories;
using S.S.L.Domain.Models;
using S.S.L.Infrastructure.S.S.L.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace S.S.L.Infrastructure.Repositories
{
    public class CustomRepository : ICustomRepository
    {
        private readonly Entities _context;

        public CustomRepository(Entities context)
        {
            _context = context;

        }


        /// <summary>
        /// Add new todo to database
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<TodoModel> AddTodoAsync(TodoModel model, int userId)
        {

            var todo = new Todo
            {
                Item = model.Item,
                Done = model.Done,
                UserId = userId
            };

            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();

            //return new TodoModel
            //{
            //    Item = todo.Item,
            //    Done = todo.Done,
            //    TodoId = todo.Id
            //};

            model.TodoId = todo.Id;
            return model;
        }

        /// <summary>
        /// Gets all the todo items belonging to a user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<TodoModel>> GetTodosAsync(int userId)
        {
            var todos = await _context.Todos
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.UpdatedAt)
                .ToListAsync();


            if (!todos.Any()) return null;


            var todoList = todos
                .SkipWhile(t => t.Done && t.UpdatedAt <= DateTime.Now.AddMinutes(-2))
                .Take(10)
                .Select(t => new TodoModel
                {
                    Item = t.Item,
                    Done = t.Done,
                    TodoId = t.Id
                })
                .ToList();

            return todoList;
        }

        public async Task<TodoModel> UpdateTodoAsync(int todoId, int userId)
        {

            var todo = await _context.Todos.Where(t => t.Id == todoId && t.UserId == userId).FirstOrDefaultAsync();
            if (todo == null || todo.Done) throw new Exception("Invalid operation");

            todo.Done = true;

            _context.Entry(todo).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return new TodoModel
            {
                Item = todo.Item,
                Done = todo.Done,
                TodoId = todo.Id
            };

        }
    }
}
