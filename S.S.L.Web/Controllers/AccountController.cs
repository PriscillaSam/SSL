using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using S.S.L.Domain.Enums;
using S.S.L.Domain.Managers;
using S.S.L.Domain.ModelExtensions;
using S.S.L.Domain.Models;
using S.S.L.Infrastructure.Services.EmailService;
using S.S.L.Web.Models.AuthViewModels;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace S.S.L.Web.Controllers
{

    [RoutePrefix("accounts")]
    public class AccountController : Controller
    {
        public IAuthenticationManager AuthManager => HttpContext.GetOwinContext().Authentication;

        private readonly UserManager _user;
        private readonly EmailGenerator _email;
        private readonly CustomManager _location;
        private readonly MenteeManager _mentee;

        public AccountController(UserManager user, CustomManager location, EmailGenerator email, MenteeManager mentee)
        {
            _user = user;
            _email = email;
            _location = location;
            _mentee = mentee;
        }

        [Route("login")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {

            try
            {
                var user = await _user.Login(model.Email, model.Password);
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.FirstName),
                    new Claim(ClaimTypes.GivenName, user.FullName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Actor, user.UserType.ToString())
                };


                foreach (var roleClaim in user.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, roleClaim));
                }

                var userIdentity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                var props = new AuthenticationProperties
                {
                    IsPersistent = model.RememberMe,
                };

                AuthManager.SignIn(props, userIdentity);

                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("index", user.UserType.ToString());
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View();

        }

        public ActionResult LogOut()
        {
            AuthManager.SignOut();
            return RedirectToAction("login");
        }

        [Route("register")]
        public async Task<ActionResult> SignUp()
        {
            await PopulateLocationDropdown();
            return View();
        }

        [Route("register")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignUp(SignUpViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new UserModel
                    {
                        UserType = UserType.Mentee,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        EmploymentStatus = model.EmploymentStatus,
                        Gender = model.Gender,
                        Country = model.Country,
                        State = model.State,
                        MobileNumber = model.MobileNumber,

                    };

                    var newUser = await _user.RegisterAsync(user, model.Password);
                    await _mentee.CreateMentee(newUser.Id);

                    //send email
                    var email = _email.GetTemplate(EmailType.AccountConfirmation, newUser);
                    await email.GenerateEmailAsync();

                    ViewBag.Success = "An email has been sent to you to confirm your account.";

                }

            }
            catch (Exception ex)
            {
                await PopulateLocationDropdown();
                ViewBag.Error = ex.Message;
            }

            await PopulateLocationDropdown();
            return View();

        }

        //Confirmation page get
        [Route("{userId}/confirm")]
        public ActionResult AccountConfirmation(int userId)
        {
            ViewBag.UserId = userId;
            return View();
        }

        [Route("confirm")]
        public async Task<JsonResult> Confirm(int userId)
        {
            string msg;
            try
            {
                var user = await _user.ConfirmUser(userId);

                //send welcome mail if user is a mentee
                if (user.IsMentee())
                {
                    var email = _email.GetTemplate(EmailType.MenteeWelcome, user);
                    var email2 = _email.GetTemplate(EmailType.MenteeNextSteps, user);

                    await email.GenerateEmailAsync();
                    await email2.GenerateEmailAsync();
                }

                if (user.IsFacilitator())
                {
                    var email = _email.GetTemplate(EmailType.FacilitatorWelcome, user);
                    await email.GenerateEmailAsync();
                }

                msg = "Your account has been verified";
                return Json(msg, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        //password reset
        [Route("forgot")]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [Route("forgot")]
        [HttpPost]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //check that email exists
                    var user = await _user.ValidateEmail(model.Email);
                    //send email
                    var resetEmail = _email.GetTemplate(EmailType.PasswordReset, user);
                    await resetEmail.GenerateEmailAsync();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View(model);
        }

        [Route("reset")]
        public ActionResult ResetPassword()
        {
            return View();
        }

        [Route("reset")]
        [HttpPost]
        public async Task<ActionResult> ResetPassword(PasswordResetViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _user.RecoverPasswordAsync(model.Email, model.Password);
                }

                ViewBag.Success = "Your password has been changed successfully";

            }

            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View();
        }

        [Route("resend")]
        public ActionResult ResendConfirmationEmail()
        {
            return View();
        }


        [Route("resend")]
        [HttpPost]
        public async Task<JsonResult> ResendConfirmationEmail(string email)
        {
            try
            {

                var user = await _user.ValidateEmail(email);

                var confirmEmail = _email.GetTemplate(EmailType.AccountConfirmation, user);
                await confirmEmail.GenerateEmailAsync();

                return Json("Please check your mail box. An email has been sent to you.", JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);

            }

        }

        private async Task PopulateLocationDropdown()
        {
            ViewBag.Countries = new SelectList(await _location.GetCountries(), "Name", "Name");
            ViewBag.States = new SelectList(await _location.GetStates(1), "Name", "Name");
        }
    }
}