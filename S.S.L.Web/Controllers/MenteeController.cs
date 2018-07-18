using Microsoft.AspNet.Identity;
using S.S.L.Domain.Enums;
using S.S.L.Domain.Managers;
using S.S.L.Domain.Models;
using S.S.L.Web.Models.CustomViewModels;
using S.S.L.Web.Models.MenteeViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace S.S.L.Web.Controllers
{
    [Authorize]
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
        [Authorize(Roles = nameof(UserType.Mentee))]
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
        [Authorize(Roles = nameof(UserType.Mentee))]

        public async Task<ActionResult> UserProfile()
        {
            var userId = int.Parse(User.Identity.GetUserId());
            var model = new MenteeProfileViewModel
            {
                Mentee = await _mentee.GetMentee(userId),
                Facilitator = await _mentee.GetMenteeFacilitator(userId)
            };
            await PopulateLocationDropdown();

            return View(model);
        }

        [Authorize(Roles = nameof(UserType.Mentee))]
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
            await PopulateLocationDropdown();
            return View(viewModel);

        }


        private async Task PopulateLocationDropdown()
        {
            ViewBag.Countries = new SelectList(await _custom.GetCountries(), "Name", "Name");
            ViewBag.States = new SelectList(await _custom.GetStates(1), "Name", "Name");

        }
    }
}