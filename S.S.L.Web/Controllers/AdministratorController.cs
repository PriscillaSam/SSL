﻿using Microsoft.AspNet.Identity;
using S.S.L.Domain.Enums;
using S.S.L.Domain.Managers;
using S.S.L.Domain.ModelExtensions;
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
            if (!User.Identity.IsSuperAdmin())
                return RedirectToAction("index", User.Identity.GetUserType());

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

        [HttpGet]
        [Route("profile/{userId}")]
        public async Task<ActionResult> UserProfile(int userId)
        {

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
            var model = new MenteesViewModel
            {
                Mentees = await _mentee.GetMentees(mentored),

            };
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
                //get the facilitator and mentee
                var mentee = await _user.GetUserById(model.MenteeId);
                var facilitator = await _user.GetUserById(model.FacilitatorId);

                var emailModel = new MenteeFacilitatorModel
                {
                    MenteeName = mentee.FullName,
                    MenteeEmail = mentee.Email,
                    MenteeFirstName = mentee.FirstName,
                    MenteeNumber = mentee.MobileNumber,
                    FacilitatorEmail = facilitator.Email,
                    FacilitatorName = facilitator.FullName,
                    FacilitatorFirstName = facilitator.FirstName
                };

                var email = _emailService.GetTemplate(EmailType.MenteeAssignment, emailModel);
                await email.GenerateEmailAsync();

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

        [Route("gym-groups")]
        public async Task<ActionResult> GymGroups()
        {
            var model = await _user.GetGymGroupsAsync();
            return View(model);
        }

        [Route("deleted")]
        public async Task<ActionResult> RemovedUsers()
        {
            var model = await _user.DeletedUsers();
            return View(model);
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
                var user = await _user.GetUserById(userId);

                if (user.IsFacilitator())
                {
                    var email = _emailService.GetTemplate(EmailType.FacilitatorWelcome, user);
                    await email.GenerateEmailAsync();
                }
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

        [Route("gym/remove")]
        [HttpPost]
        public async Task<JsonResult> GymRemove(int userId)
        {
            try
            {
                await _user.RemoveGymMembership(userId);
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);

            }

        }
        [Route("gym/assign")]
        [HttpPost]
        public async Task<JsonResult> AssignGymGroup(int userId, GymGroup group)
        {
            try
            {
                await _user.AddGymMember(userId, group);
                return Json(group.GetDescription(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);

            }
        }

        [Route("restore")]
        [HttpPost]
        public async Task<JsonResult> Restore(int userId)
        {
            try
            {
                await _user.RestoreUser(userId);
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);

            }
        }


        #endregion

        private async Task PopulateLocationDropdown()
        {
            ViewBag.Countries = new SelectList(await _custom.GetCountries(), "Name", "Name");
            ViewBag.States = new SelectList(await _custom.GetStates(1), "Name", "Name");
        }
    }
}