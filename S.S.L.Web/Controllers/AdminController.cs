using Microsoft.AspNet.Identity;
using S.S.L.Domain.Enums;
using S.S.L.Domain.Managers;
using S.S.L.Domain.Models;
using S.S.L.Web.Models.AdminViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace S.S.L.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    [RoutePrefix("admin")]
    public class AdminController : Controller
    {
        private readonly UserManager _user;
        private readonly CustomManager _custom;

        public AdminController(UserManager user, CustomManager custom)
        {
            _user = user;
            _custom = custom;
        }

        // GET: Admin
        [Route("dashboard")]
        public async Task<ActionResult> Index()
        {
            var mentees = await _user.GetUsersAsync(UserType.Mentee);
            var facilitators = await _user.GetUsersAsync(UserType.Facilitator);
            var todos = await _custom.GetUserTodos(int.Parse(User.Identity.GetUserId()));

            if (todos == null) todos = new List<TodoModel>();
            var model = new DashboardViewModel
            {
                Mentees = mentees.Count,
                Facilitatators = facilitators.Count,
                Todos = todos
            };

            //forum counts,
            return View(model);

        }

        [Route("todo/create")]
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

        [Route("todo/update")]
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

        [Route("manage/facilitators")]
        public async Task<ActionResult> Facilitators()
        {
            //list of facilitators
            var model = new FacilitatorsViewModel
            {
                Facilitators = await _user.GetUsersAsync(UserType.Facilitator)
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
                };

                await _user.RegisterFacilitator(newMentor, model.MakeAdmin);

                ViewBag.Success = "Facilitator account created";

            }

            catch (Exception ex)
            {

                ViewBag.Error = ex.Message;
            }
            //list of facilitators
            var viewModel = new FacilitatorsViewModel
            {
                Facilitators = await _user.GetUsersAsync(UserType.Facilitator)
            };

            return View(viewModel);
        }

        [Route("manage/mentees")]
        public async Task<ActionResult> Mentees()
        {
            var model = await _user.GetUsersAsync(UserType.Mentee);
            return View(model);
        }

    }
}