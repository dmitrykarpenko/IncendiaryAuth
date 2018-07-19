using IncendiaryAuth.Domain.Abstract.Models;
using IncendiaryAuth.Domain.Abstract.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncendiaryAuth.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthLogic _authLogic;

        public AuthController(
            IAuthLogic authLogic)
        {
            _authLogic = authLogic;
        }

        [HttpGet]
        public new IActionResult User()
        {
            var userauth = new UserAuthModel { UserName = "test user" };

            return View(userauth);
        }

        [HttpPost]
        public new JsonResult User(UserAuthModel userAuth)
        {
            var user = _authLogic.AuthenticateUser(userAuth);
            return Json(user);
        }
    }
}
