using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuickKartDataAccessLayer;
using QuickKartMVCApp.Repositary;

namespace QuickKartMVCApp.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult ViewProducts()
        {
            QuickKartRepository rep = new QuickKartRepository();
            List<Product> prodList = rep.GetProductUsingLinq();
            List<Models.Product> mProdtList = new List<Models.Product>();
            MyMapper<Product, Models.Product> mapObj = new MyMapper<Product, Models.Product>();
            foreach(var item in prodList)
            {
                mProdtList.Add(mapObj.Translate(item));
            }

            return View(mProdtList);
        }
        public ActionResult AddProduct()
        {
            QuickKartRepository repobj = new QuickKartRepository();
            string productId = repobj.GetNewProductIdUsingUFN();
            ViewBag.NextProductId = productId;
            return View();
        }

        [HttpPost]
        public ActionResult SaveAddedProduct(Models.Product prodObj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    QuickKartRepository repobj = new QuickKartRepository();
                    MyMapper<Models.Product, Product> mapObj = new MyMapper<Models.Product, Product>();
                    var status = repobj.AddProduct(mapObj.Translate(prodObj));
                    if (status)
                        return RedirectToAction("ViewProducts");
                    else
                        return View("Error");
                }
                catch (Exception)
                {
                    return View("Error");
                }
            }
            return View("AddProduct", prodObj);
        }

        public ActionResult UpdateResult(Models.Product pr)
        {
            return View(pr);
        }

        public ActionResult SaveUpdatedResult(Models.Product pr)
        {
            if (ModelState.IsValid)
            {
                try {
                    QuickKartRepository rep = new QuickKartRepository();
                    MyMapper<Models.Product, Product> mapObj = new MyMapper<Models.Product, Product>();
                    bool status=rep.UpdateProduct(mapObj.Translate(pr));
                    if (status)
                        return RedirectToAction("ViewProducts");
                    else
                        return View("Error");
                }
                catch(Exception ex)
                {
                    return View("Error");
                }

            }
            return View("UpdateResults",pr);
        }

        public ActionResult DeleteProduct(Models.Product pr)
        {
            return View(pr);
        }

        public ActionResult SaveDeletedProduct(Models.Product pr)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    QuickKartRepository rep = new QuickKartRepository();
                    MyMapper<Models.Product, Product> mapObj = new MyMapper<Models.Product, Product>();
                    bool status = rep.DeleteProduct(mapObj.Translate(pr));

                    if (status)
                    {
                        return RedirectToAction("ViewProducts");
                    }
                    else
                        return View("Error");
                }
                catch (Exception ex)
                {
                    return View("Error");
                }
            }
            return View("DeleteProduct",pr);
        }
        public ActionResult GetProductForCategory(byte? categoryId)
        {
            QuickKartRepository repObj = new QuickKartRepository();
            ViewBag.CategoryList = repObj.GetCategoriesUsinglinq();
            if (categoryId != null)
            {
                Session["categoryId"] = categoryId;
            }
            else
            {
                categoryId = Convert.ToByte(Session["categoryId"]);
            }
            ViewBag.SelectedCategory = repObj.GetCategoriesUsinglinq().Where(x => x.CategoryId == categoryId).Select(x => x.CategoryName).FirstOrDefault();
            if (ViewBag.SelectedCategory == null)
                ViewBag.SelectedCategory = "--Select--";
            var productList = repObj.GetProductUsingLinq();
            var mapObj = new MyMapper<Product, Models.Product>();
            var products = new List<Models.Product>();
            foreach (var product in productList)
            {
                products.Add(mapObj.Translate(product));
            }
            var filteredProducts = products.Where(model => model.CategoryId == categoryId);
            return View(filteredProducts);
        }


    }
}