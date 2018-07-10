using Microsoft.AspNet.Identity;
using S.S.L.Domain.Enums;
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
    [Authorize(Roles = nameof(UserType.Mentee))]
    [RoutePrefix("mentee")]
    public class MenteeController : Controller
    {
        private readonly CustomManager _custom;
        private readonly UserManager _user;
        private readonly MenteeManager _mentee;

        public MenteeController(CustomManager custom, UserManager user, MenteeManager mentee)
        {
            _custom = custom;
            _user = user;
            _mentee = mentee;

        }
        // GET: Mentee
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

        [Route("profile")]
        public async Task<ActionResult> UserProfile()
        {
            var userId = int.Parse(User.Identity.GetUserId());
            var model = new MenteeProfileViewModel
            {
                Mentee = await _mentee.GetMentee(userId),
                Facilitator = await _mentee.GetMenteeFacilitator(userId)
            };

            return View(model);
        }


        [Route("profile")]
        [HttpPost]
        public async Task<ActionResult> UserProfile(UserModel model)
        {
            var userId = int.Parse(User.Identity.GetUserId());

            await _user.UpdateProfile(userId, model);

            var viewModel = new MenteeProfileViewModel
            {
                Mentee = await _mentee.GetMentee(userId),
                Facilitator = await _mentee.GetMenteeFacilitator(userId)
            };

            return View(viewModel);        

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