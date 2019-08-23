using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using QuickKartDataAccessLayer;
namespace QuickKartMVCApp.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        QuickKartRepository rep;
        public AdminController()
        {
            rep = new QuickKartRepository();
        }
        public ActionResult AdminHome()
        {
            List<string> lstProducts = rep.FetchTopProducts();
           
            ViewBag.TopProducts = lstProducts;
            return View();
        }
       public ContentResult EmployeeInfo() 
        {
            ContentResult x = new ContentResult();
             x.Content="<Employees><EId>101</EId><EName>taha</EName><EId>102</EId><EName>talha</EName></Employees>";
            x.ContentType = "text/xml";
            
            return x;
        }
    }
}