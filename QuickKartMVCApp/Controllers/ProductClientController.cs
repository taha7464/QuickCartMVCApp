using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using QuickKartMVCApp.Repository;

namespace QuickKartMVCApp.Controllers
{
    public class ProductClientController : Controller
    {
        // GET: Purchase
        public ActionResult Index()
        {
            //try
            //{
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse("api/Product/GetProducts/");
            response.EnsureSuccessStatusCode();
            var product = response.Content.ReadAsAsync<IEnumerable<Models.Product>>().Result;
            ViewBag.Title = "Index";
            return View(product);
        }
        public ActionResult Details(string id)
        {
            //try
            //{
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse("api/Product/GetProduct/"+id);
            response.EnsureSuccessStatusCode();
            var product = response.Content.ReadAsAsync<Models.Product>().Result;
            ViewBag.Title = "Details";
            return View(product);
        }
        //catch (Exception ex)
        //{
        //    return View("Error");
        //}

    }
}

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace QuickKartMVCApp.Controllers
//{
//    public class ProductClientController : Controller
//    {
//        // GET: ProductClient
//        public ActionResult Index()
//        {
//            return View();
//        }

//        // GET: ProductClient/Details/5
//        public ActionResult Details(int id)
//        {
//            return View();
//        }

//        // GET: ProductClient/Create
//        public ActionResult Create()
//        {
//            return View();
//        }

//        // POST: ProductClient/Create
//        [HttpPost]
//        public ActionResult Create(FormCollection collection)
//        {
//            try
//            {
//                // TODO: Add insert logic here

//                return RedirectToAction("Index");
//            }
//            catch
//            {
//                return View();
//            }
//        }

//        // GET: ProductClient/Edit/5
//        public ActionResult Edit(int id)
//        {
//            return View();
//        }

//        // POST: ProductClient/Edit/5
//        [HttpPost]
//        public ActionResult Edit(int id, FormCollection collection)
//        {
//            try
//            {
//                // TODO: Add update logic here

//                return RedirectToAction("Index");
//            }
//            catch
//            {
//                return View();
//            }
//        }

//        // GET: ProductClient/Delete/5
//        public ActionResult Delete(int id)
//        {
//            return View();
//        }

//        // POST: ProductClient/Delete/5
//        [HttpPost]
//        public ActionResult Delete(int id, FormCollection collection)
//        {
//            try
//            {
//                // TODO: Add delete logic here

//                return RedirectToAction("Index");
//            }
//            catch
//            {
//                return View();
//            }
//        }
//    }
//}
