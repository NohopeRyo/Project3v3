using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Project_3_v3.Models;
using System.Linq;

namespace Project_3_v3.Controllers
{
    public class LoginController : Controller
    {
        dataver2Entities entities= new dataver2Entities();
        // GET: AdminShop/Login
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Register() { return View(); }
        [HttpPost]
        public ActionResult Login(LoginViewModel credentials)
        {

            bool userExist = entities.Users.Any(x => x.Email == credentials.Email && x.Password == credentials.Password);
            User u = entities.Users.FirstOrDefault(x => x.Email == credentials.Email && x.Password == credentials.Password);
            if (userExist)
            {
                FormsAuthentication.SetAuthCookie(u.UserName, false);
                return RedirectToAction("Index","Home");
            }
            ModelState.AddModelError("", "Username or Password is wrong");
            return View();
        }
        [HttpPost]
        public ActionResult Register(User user) 
        { 
            entities.Users.Add(user);
            entities.SaveChanges();
            return RedirectToAction("Login"); 
        }
        public ActionResult Logout() 
        { 
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Home"); 
        }
    }
}