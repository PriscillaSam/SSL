using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S.S.L.Web.Controllers
{
    [RoutePrefix("home")]
    public class HomeController : Controller
    {
        // GET: Home
        [Authorize]
        [Route("~/", Name ="index")]
        public ContentResult Index()
        {
            return Content("Welcome to the beginning of nothingness");
        }
    }
}