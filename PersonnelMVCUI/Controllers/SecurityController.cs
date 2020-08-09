using PersonnelMVCUI.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.Security;

namespace PersonnelMVCUI.Controllers
{
        [AllowAnonymous]
    public class SecurityController : Controller
    {
        DbPersonnelEntities db = new DbPersonnelEntities();
        // GET: Security
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            var userdb = db.User.FirstOrDefault(m => m.Name == user.Name && m.Password == user.Password);
            if (userdb!=null)
            {
                FormsAuthentication.SetAuthCookie(userdb.Name, false);
                return RedirectToAction("Index","Department");
            }
            else
            {
                ViewBag.Message = "Invalid user name or password";
                return View();
            }
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}