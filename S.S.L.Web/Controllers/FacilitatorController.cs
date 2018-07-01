using Microsoft.AspNet.Identity;
using S.S.L.Domain.Managers;
using S.S.L.Domain.Models;
using S.S.L.Web.Models.CustomViewModels;
using S.S.L.Web.Models.FacilitatorViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using static S.S.L.Domain.Enums.UserType;
namespace S.S.L.Web.Controllers
{
    [Authorize(Roles = nameof(Facilitator))]
    [RoutePrefix("facilitator")]
    public class FacilitatorController : Controller
    {
        private readonly UserManager _user;
        private readonly CustomManager _custom;

        public FacilitatorController(UserManager user, CustomManager custom)
        {
            _user = user;
            _custom = custom;
        }

        [Route("dashboard")]
        public async Task<ActionResult> Index()
        {
            var todos = await _custom.GetUserTodos(int.Parse(User.Identity.GetUserId()));

            if (todos == null) todos = new List<TodoModel>();
            var model = new DashViewModel
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