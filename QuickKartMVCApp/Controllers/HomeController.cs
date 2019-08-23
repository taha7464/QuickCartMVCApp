using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuickKartDataAccessLayer;
namespace QuickKartMVCApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Default
        public ActionResult index()
        {
           
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        public ActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckRole(FormCollection frm)
        {
            QuickKartRepository repObj = new QuickKartRepository();
            string userId = frm["name"];
            string password = frm["pwd"];
            string checkBox = frm["Rememberme"];
            byte? roleId = repObj.GetRoleIdByUserId(userId);

           
            if (checkBox != "false")
            {
                HttpCookie cookieObj = new HttpCookie("User");
                cookieObj.Values.Add("User", userId);
                cookieObj.Values.Add("Password", password);
                cookieObj.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(cookieObj);
            }
            
            if (repObj.ValidateCredentials(userId, password))
            {
                string lastLogin=null;
                HttpCookie loginInCookie=new HttpCookie(userId);
                if (Request.Cookies[userId] != null)
                {
                     loginInCookie =Request.Cookies[userId];
                    TempData["lastLogin"] = loginInCookie.Values["lastLogin"];
                }
                loginInCookie["lastLogin"] = DateTime.Now.ToString();
                loginInCookie.Expires = DateTime.MaxValue;
                Response.Cookies.Add(loginInCookie);
                

                if (roleId == 1)
                {
                    Session["UserId"] = userId;
                    return RedirectToAction("AdminHome", "Admin");
                }
                else if (roleId == 2)
                {
                    
                    return Redirect("/Customer/CustomerHome?user="+userId);
                }
            }
            return View("Login");

        }
        public JsonResult GetCoupons()
        {
            Random random = new Random();
            Dictionary<string, string> data = new Dictionary<string, string>();
            string[] value = new String[5];
            string[] key = { "Arts", "Electronics", "Fashion", "Home", "Toys" };
            for (int i = 0; i < 5; i++)
            {
                string number = "RUSH" + random.Next(1, 10).ToString() + random.Next(1, 10).ToString() + random.Next(1, 10).ToString();
                value[i] = number;
            }
            for (int i = 0; i < 5; i++)
            {
                data.Add(key[i], value[i]);
            }
            return Json(data,JsonRequestBehavior.AllowGet);
        }
        public FilePathResult DownloadTermsAndConditions()
        {
            return File(@"..\Downloads\TermsAndConditions.pdf", "pdf");
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult FAQ()
        {
            return RedirectToAction("Contact", "Home");
        }

    }
}