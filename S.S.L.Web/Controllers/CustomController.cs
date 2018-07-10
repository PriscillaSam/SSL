using Microsoft.AspNet.Identity;
using S.S.L.Domain.Managers;
using S.S.L.Domain.Models;
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



    }
}