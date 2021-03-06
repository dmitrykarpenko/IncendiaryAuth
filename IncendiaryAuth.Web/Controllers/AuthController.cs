﻿using IncendiaryAuth.Domain.Abstract.Models;
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public new JsonResult User(UserAuthModel userAuth)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { Error = string.Join(", ", ModelStateErrors) });
            }

            var user = _authLogic.AuthenticateUser(userAuth);
            return Json(user);
        }

        private IEnumerable<string> ModelStateErrors =>
            ModelState
                .SelectMany(ms => ms.Value.Errors)
                .Select(me => me.ErrorMessage);

        [HttpGet]
        public new IActionResult NotFound()
        {
            return View();
        }
    }
}
