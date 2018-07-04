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
        private readonly CustomManager _custom;
        private readonly FacilitatorManager _facilitator;

        public FacilitatorController(CustomManager custom, FacilitatorManager facilitator)
        {
            _custom = custom;
            _facilitator = facilitator;
        }

        [Route("dashboard")]
        public async Task<ActionResult> Index()
        {
            var userId = int.Parse(User.Identity.GetUserId());
            var todos = await _custom.GetUserTodos(userId);
            var mentees = await _facilitator.GetFacilitatorMentees(userId);
            if (todos == null) todos = new List<TodoModel>();
            var model = new DashViewModel
            {
                Mentees = mentees.Count,
                TodoModel = new TodoViewModel
                {
                    Todos = todos
                }
            };

            return View(model);
        }

        //[Authorize(Roles = nameof(Administrator))]

    }
}