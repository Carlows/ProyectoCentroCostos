using CentroCostos.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CentroCostos.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _users { get; set; }

        public AccountController(IUserService users)
        {
            _users = users;
        }

        // GET: Account login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Account login
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            bool isUserValid = _users.Authenticate(username, password);

            if (isUserValid)
            {
                FormsAuthentication.SetAuthCookie(username, false);
                return RedirectToAction("Index", "Administracion");
            }

            ModelState.AddModelError("", "Las credenciales insertadas son invalidas");

            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login");
        }
    }
}