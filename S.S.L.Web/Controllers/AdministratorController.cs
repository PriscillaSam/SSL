using Microsoft.AspNet.Identity;
using S.S.L.Domain.Enums;
using S.S.L.Domain.Managers;
using S.S.L.Domain.Models;
using S.S.L.Web.Infrastructure.Extensions;
using S.S.L.Web.Models.AdminViewModels;
using S.S.L.Web.Models.CustomViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace S.S.L.Web.Controllers
{
    [Authorize(Roles = nameof(UserType.Administrator))]
    [RoutePrefix("admin")]
    public class AdministratorController : Controller
    {
        private readonly UserManager _user;
        private readonly CustomManager _custom;
        private readonly MenteeManager _mentee;
        private readonly FacilitatorManager _facilitator;

        public AdministratorController(UserManager user, CustomManager custom, MenteeManager mentee, FacilitatorManager facilitator)
        {
            _user = user;
            _custom = custom;
            _mentee = mentee;
            _facilitator = facilitator;
        }

        // GET: Admin
        [Route("dashboard")]
        public async Task<ActionResult> Index()
        {
            var mentees = await _user.GetUsersAsync(UserType.Mentee);
            var facilitators = await _user.GetUsersAsync(UserType.Facilitator);
            var todos = await _custom.GetUserTodos(int.Parse(User.Identity.GetUserId()));

            if (todos == null) todos = new List<TodoModel>();
            var model = new DashboardViewModel
            {
                Mentees = mentees.Count,
                Facilitatators = facilitators.Count,
                TodoModel = new TodoViewModel
                {
                    Todos = todos
                }
            };

            //forum counts,
            return View(model);

        }


        [Route("manage/facilitators")]
        public async Task<ActionResult> Facilitators()
        {
            //list of facilitators
            var model = new FacilitatorsViewModel
            {
                Facilitators = await _user.GetUsersAsync(UserType.Facilitator)
            };

            return View(model);
        }

        [Route("manage/facilitators")]
        [HttpPost]

        public async Task<ActionResult> Facilitators(FacilitatorsViewModel model)
        {
            try
            {

                var newMentor = new UserModel
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                };

                await _user.RegisterFacilitator(newMentor, model.MakeAdmin);

                ViewBag.Success = "Facilitator account created";

            }

            catch (Exception ex)
            {

                ViewBag.Error = ex.Message;
            }
            //list of facilitators
            var viewModel = new FacilitatorsViewModel
            {
                Facilitators = await _user.GetUsersAsync(UserType.Facilitator)
            };

            return View(viewModel);
        }

        [Route("manage/mentees")]
        public async Task<ActionResult> Mentees()
        {
            var mentored = true;
            var model = await _mentee.GetMentees(mentored);
            return View(model);
        }

        [Route("mentees/assign")]
        public async Task<ActionResult> NotMentored()
        {

            var mentored = false;
            var mentees = await _mentee.GetMentees(mentored);
            var facilitators = new SelectList(await _facilitator.GetFacilitators(), "Id", "FullName");

            var model = new UnassignedMenteeViewModel
            {
                Mentees = mentees,
                Facilitators = facilitators
            };

            return View(model);
        }

        [Route("remove")]
        [HttpPost]
        public async Task<JsonResult> RemoveUser(int userId)
        {
            try
            {
                if (!User.IsAdmin())
                    throw new Exception("Unauthorized");

                await _user.RemoveUser(userId);
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [Route("changerole")]
        [HttpPost]
        public async Task<JsonResult> ChangeRole(int userId)
        {
            try
            {
                if (!User.IsAdmin())
                    throw new Exception("Unauthorized");

                await _user.UpdateUserRole(userId);
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}