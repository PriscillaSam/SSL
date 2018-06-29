using Microsoft.AspNet.Identity;
using S.S.L.Domain.Managers;
using S.S.L.Domain.Models;
using S.S.L.Web.Models.CustomViewModels;
using S.S.L.Web.Models.MenteeViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace S.S.L.Web.Controllers
{
    [RoutePrefix("mentee")]
    public class MenteeController : Controller
    {
        private readonly CustomManager _custom;

        public MenteeController(CustomManager custom)
        {
            _custom = custom;
        }
        // GET: Mentee
        [Authorize]
        [Route("dashboard")]
        public async Task<ActionResult> Index()
        {
            //get todos
            var todos = await _custom.GetUserTodos(int.Parse(User.Identity.GetUserId()));
            if (todos == null) todos = new List<TodoModel>();

            //get thought
            var model = new MenteeDashboardViewModel
            {
                TodoModel = new TodoViewModel
                {
                    Todos = todos
                }
            };

            return View(model);
        }

    }
}