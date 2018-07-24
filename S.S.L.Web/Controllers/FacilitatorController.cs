using Microsoft.AspNet.Identity;
using S.S.L.Domain.Managers;
using S.S.L.Domain.Models;
using S.S.L.Web.Models.CustomViewModels;
using S.S.L.Web.Models.FacilitatorViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using static S.S.L.Domain.Enums.UserType;
namespace S.S.L.Web.Controllers
{

    [Authorize(Roles = nameof(Facilitator) + "," + nameof(Administrator))]
    [RoutePrefix("facilitator")]
    public class FacilitatorController : Controller
    {
        private readonly UserManager _user;
        private readonly CustomManager _custom;
        private readonly FacilitatorManager _facilitator;

        public FacilitatorController(CustomManager custom, FacilitatorManager facilitator, UserManager user)
        {
            _user = user;
            _custom = custom;
            _facilitator = facilitator;
        }

        [Route("dashboard")]
        public async Task<ActionResult> Index()
        {
            var userId = int.Parse(User.Identity.GetUserId());
            var mentees = await _facilitator.GetFacilitatorMentees(userId);
            var todos = await _custom.GetUserTodos(userId);

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

        [HttpGet]
        [Route("profile/{userId:int}")]
        public async Task<ActionResult> UserProfile(int userId)
        {
            var id = User.Identity.GetUserId();
            var facilitator = await _user.GetUserById(userId);
            var mentees = await _facilitator.GetFacilitatorMentees(userId);

            var model = new FacilitatorProfileViewModel
            {
                Facilitator = facilitator,
                Mentees = mentees
            };
            await PopulateLocationDropdown();
            return View(model);
        }

        [Route("profile/{userId}")]
        [HttpPost]
        public async Task<ActionResult> UserProfile(UserModel model, int userId)
        {
            //var userId = int.Parse(User.Identity.GetUserId());

            await _user.UpdateProfile(userId, model);

            var facilitator = await _user.GetUserById(userId);
            var mentees = await _facilitator.GetFacilitatorMentees(userId);

            var viewModel = new FacilitatorProfileViewModel
            {
                Facilitator = facilitator,
                Mentees = mentees
            };
            await PopulateLocationDropdown();
            return View(viewModel);
        }

        [Route("mentee/update")]
        [HttpPost]
        public async Task<JsonResult> UpdateClassProgress(int menteeId)
        {
            var facilitatorId = int.Parse(User.Identity.GetUserId());
            try
            {
                await _facilitator.UpdateMenteeProgress(menteeId, facilitatorId);
                return Json("Updated", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);

            }
        }

        private async Task PopulateLocationDropdown()
        {
            ViewBag.Countries = new SelectList(await _custom.GetCountries(), "Name", "Name");
            ViewBag.States = new SelectList(await _custom.GetStates(1), "Name", "Name");

        }
    }
}