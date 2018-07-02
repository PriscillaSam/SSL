using Microsoft.AspNet.Identity;
using S.S.L.Domain.Managers;
using S.S.L.Domain.Models;
using S.S.L.Web.Infrastructure.Extensions;
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
        private readonly UserManager _user;

        public MenteeController(CustomManager custom, UserManager user)
        {
            _custom = custom;
            _user = user;
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


        [Route("get")]
        public async Task<JsonResult> Mentee(int userId)
        {
            if (!User.Identity.IsSuperAdmin()) return Json("Unauthorized", JsonRequestBehavior.AllowGet);

            var mentee = await _user.GetUserById(userId);

            return Json(mentee, JsonRequestBehavior.AllowGet);
        }

    }
}