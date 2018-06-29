using System.Web.Mvc;

namespace S.S.L.Web.Controllers
{
    public class MenteeController : Controller
    {
        // GET: Mentee
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.User = User.Identity.Name;
            return View();
        }


    }
}