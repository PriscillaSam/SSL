using Microsoft.Owin.Security;
using S.S.L.Domain.Managers;
using S.S.L.Domain.Models;
using S.S.L.Web.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace S.S.L.Web.Controllers
{

    [RoutePrefix("accounts")]
    public class AccountController : Controller
    {

        private readonly UserManager _user;
        private readonly LocationManager _location;

        public AccountController(UserManager user, LocationManager location)
        {
            _user = user;
            _location = location;
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
            var auth = HttpContext.GetOwinContext().Authentication;

            try
            {
                var user = await _user.Login(model.Email, model.Password);
                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.GivenName, user.FullName ),

                };

                var userIdentity = new ClaimsIdentity(claims);
                var props = new AuthenticationProperties
                {
                    IsPersistent = model.RememberMe
                };

                auth.SignIn(props, userIdentity);
                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);

                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            //get claims

            return View();

        }

        [Route("register")]
        public async Task<ActionResult> SignUp()
        {
            await PopulateDropdown();
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
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        EmploymentStatus = model.EmploymentStatus,
                        Gender = model.Gender,
                        Country = model.Country,
                        State = model.State,
                        MobileNumber = model.MobileNumber


                    };

                    var newUser = await _user.Register(user, model.Password);
                    return RedirectToAction("Login");

                }

            }
            catch (Exception ex)
            {
                await PopulateDropdown();
                ViewBag.Error = ex.Message;
            }

            await PopulateDropdown();
            return View();

        }


        private async Task PopulateDropdown()
        {
            ViewBag.Countries = new SelectList(await _location.GetCountries(), "Name", "Name");
            ViewBag.States = new SelectList(await _location.GetStates(1), "Name", "Name");

        }


    }
}