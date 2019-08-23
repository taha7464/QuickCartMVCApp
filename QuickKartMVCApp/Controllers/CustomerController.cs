using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuickKartDataAccessLayer;

namespace QuickKartMVCApp.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        QuickKartRepository rep;
        public CustomerController()
        {
            rep = new QuickKartRepository();
        }
        public ActionResult CustomerHome()
        {
            List<string> lstProducts = rep.FetchTopProducts(Request.QueryString["user"]);
           
            ViewBag.TopProducts = lstProducts;
            return View();
        }
    }
}