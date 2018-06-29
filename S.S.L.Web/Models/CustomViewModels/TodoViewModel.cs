using S.S.L.Domain.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace S.S.L.Web.Models.CustomViewModels
{
    public class TodoViewModel
    {
        [Required(ErrorMessage = "Todo item can't be empty")]
        public string TodoItem { get; set; }
        public IEnumerable<TodoModel> Todos { get; set; }
    }
}