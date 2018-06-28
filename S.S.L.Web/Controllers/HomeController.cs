using System.Web.Mvc;

namespace S.S.L.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        [Route("~/")]
        [Route("home/index")]
        public ActionResult Index()
        {
            return View();
        }
    }
}