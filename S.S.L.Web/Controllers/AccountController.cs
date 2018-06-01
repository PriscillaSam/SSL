using Microsoft.Owin.Security;
using S.S.L.Domain.Models;
using S.S.L.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace S.S.L.Web.Controllers
{
    [RoutePrefix("accounts")]
    public class AccountController : Controller
    {

        [Route("login")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            var auth = HttpContext.GetOwinContext().Authentication;

            var user = new UserModel();
            //get claims
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

            auth.SignIn(props ,userIdentity);
            return View();

        }
    }
}