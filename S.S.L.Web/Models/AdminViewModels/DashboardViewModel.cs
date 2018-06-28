using S.S.L.Domain.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace S.S.L.Web.Models.AdminViewModels
{
    public class DashboardViewModel
    {
        //mentee count
        //mentor count
        //forum count
        //todos
        //thought
        //

        [Required(ErrorMessage = "Todo item can't be empty")]
        public string TodoItem { get; set; }
        public int Mentees { get; set; }
        public int Facilitatators { get; set; }
        public IEnumerable<TodoModel> Todos { get; set; }

    }
}