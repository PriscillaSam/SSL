using Microsoft.AspNet.Identity;
using S.S.L.Domain.Enums;
using S.S.L.Domain.Managers;
using S.S.L.Domain.ModelExtensions;
using S.S.L.Domain.Models;
using S.S.L.Web.Infrastructure.Extensions;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace S.S.L.Web.Controllers
{
    [Authorize]

    public class CustomController : Controller
    {
        private readonly UserManager _user;
        private readonly CustomManager _custom;

        public CustomController(CustomManager custom, UserManager user)
        {
            _user = user;
            _custom = custom;
        }


        [Route("custom/todo/create")]
        [HttpPost]
        public async Task<JsonResult> CreateTodo(string todoItem)
        {
            try
            {
                var todo = new TodoModel
                {
                    Item = todoItem,
                    Done = false,
                };

                var newTodo = await _custom.CreateTodo(todo, int.Parse(User.Identity.GetUserId()));
                return Json(newTodo, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

        }

        [Route("custom/todo/update")]
        [HttpPost]
        public async Task<JsonResult> UpdateTodo(int todoId)
        {
            var userId = int.Parse(User.Identity.GetUserId());
            try
            {
                var todo = await _custom.UpdateTodo(todoId, userId);
                return Json(todo, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }


        [Route("custom/user")]
        [Authorize(Roles = nameof(UserType.Administrator))]
        public async Task<JsonResult> GetUser(int userId)
        {
            if (!User.IsAdmin()) return Json("Unauthorized", JsonRequestBehavior.AllowGet);

            var user = await _user.GetUserById(userId);

            return Json(user, JsonRequestBehavior.AllowGet);
        }


        [Route("gym-group/members")]
        public async Task<ActionResult> GymMembers()
        {
            var userId = int.Parse(User.Identity.GetUserId());

            var user = await _user.GetUserById(userId);

            if (!user.HasGymGroup())
            {
                ViewBag.Message = "You have not been assigned a gym group yet";
                ViewBag.Header = "Pending";
            }

            else
            {
                ViewBag.Header = user.GymGroup.GetDescription();
            }
            var model = await _user.GetGymMembers(userId);
            return View(model);
        }
    }
}