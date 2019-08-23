using QuickKartMVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuickKartMVCApp.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult RegisterUser()
        {
            return View();
        }
        public ActionResult SaveRegisterUser(User user)
        {
            if (ModelState.IsValid)
            {
                QuickKartDataAccessLayer.QuickKartRepository rep = new QuickKartDataAccessLayer.QuickKartRepository();
                if (rep.RegisterUser(user.EmailId, user.UserPassword, user.Gender, user.DateOfBirth, user.Address))
                    return RedirectToAction("Login","Home");
                else
                    return View("Error");
            }
            else
            {
                return View("RegisterUser");
            }
        }
    }
}