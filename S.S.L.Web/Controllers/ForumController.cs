using System.Web.Mvc;
using static S.S.L.Domain.Enums.UserType;

namespace S.S.L.Web.Controllers
{
    [RoutePrefix("forum")]
    public class ForumController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = nameof(Administrator))]
        [Route("create")]
        public ActionResult Create()
        {
            return View();
        }
    }
}