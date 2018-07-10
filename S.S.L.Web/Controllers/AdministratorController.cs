using Microsoft.AspNet.Identity;
using S.S.L.Domain.Enums;
using S.S.L.Domain.Managers;
using S.S.L.Domain.Models;
using S.S.L.Infrastructure.Services.EmailService;
using S.S.L.Web.Infrastructure.Extensions;
using S.S.L.Web.Models.AdminViewModels;
using S.S.L.Web.Models.CustomViewModels;
using S.S.L.Web.Models.FacilitatorViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using static S.S.L.Domain.Enums.UserType;
namespace S.S.L.Web.Controllers
{
    [Authorize(Roles = nameof(Administrator))]
    [RoutePrefix("admin")]
    public class AdministratorController : Controller
    {
        private readonly UserManager _user;
        private readonly CustomManager _custom;
        private readonly MenteeManager _mentee;
        private readonly FacilitatorManager _facilitator;
        private readonly EmailGenerator _emailService;

        public AdministratorController(UserManager user, CustomManager custom, MenteeManager mentee, FacilitatorManager facilitator)
        {
            _user = user;
            _custom = custom;
            _mentee = mentee;
            _facilitator = facilitator;
            _emailService = new EmailGenerator();
        }

        // GET: Admin
        [Route("dashboard")]
        public async Task<ActionResult> Index()
        {
            var mentees = await _user.GetUsersAsync(Mentee);
            var facilitators = await _user.GetUsersAsync(Facilitator);
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

        [Route("profile")]
        public async Task<ActionResult> UserProfile()
        {
            var userId = int.Parse(User.Identity.GetUserId());

            var facilitator = await _user.GetUserById(userId);
            var mentees = await _facilitator.GetFacilitatorMentees(userId);

            var model = new FacilitatorProfileViewModel
            {
                Facilitator = facilitator,
                Mentees = mentees
            };

            return View(model);
        }

        [Route("profile")]
        [HttpPost]
        public async Task<ActionResult> UserProfile(UserModel model)
        {
            var userId = int.Parse(User.Identity.GetUserId());

            await _user.UpdateProfile(userId, model);

            var facilitator = await _user.GetUserById(userId);
            var mentees = await _facilitator.GetFacilitatorMentees(userId);

            var viewModel = new FacilitatorProfileViewModel
            {
                Facilitator = facilitator,
                Mentees = mentees
            };

            return View(viewModel);
        }

        [Route("manage/facilitators")]
        public async Task<ActionResult> Facilitators()
        {
            //list of facilitators
            var model = new FacilitatorsViewModel
            {
                Facilitators = await _user.GetUsersAsync(Facilitator)
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
                    Gender = model.Gender
                };

                var facilitator = await _user.RegisterFacilitator(newMentor, model.MakeAdmin);
                var email = _emailService.GetTemplate(EmailType.AccountConfirmation, facilitator);
                await email.GenerateEmailAsync();

                ViewBag.Success = "Facilitator account created. Awaiting email confirmation from facilitator";

            }

            catch (Exception ex)
            {

                ViewBag.Error = ex.Message;
            }
            //list of facilitators
            var viewModel = new FacilitatorsViewModel
            {
                Facilitators = await _user.GetUsersAsync(Facilitator)
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

            var model = new UnassignedMenteeViewModel
            {
                Mentees = mentees,
            };

            return View(model);
        }

        [Route("mentees/assign")]
        [HttpPost]
        public async Task<ActionResult> NotMentored(UnassignedMenteeViewModel model)
        {
            try
            {

                await _mentee.AssignFacilitator(model.MenteeId, model.FacilitatorId);
                ViewBag.Success = "Mentee assigned";
            }

            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            var mentored = false;
            var mentees = await _mentee.GetMentees(mentored);

            var viewModel = new UnassignedMenteeViewModel
            {
                Mentees = mentees,
            };
            return View(viewModel);
        }


        #region Ajax Method Calls

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

        [Route("get")]
        [HttpPost]
        public async Task<JsonResult> FacilitatorList(string gender)
        {
            var facilitators = await _facilitator.GetFacilitators(gender);

            return Json(facilitators, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}