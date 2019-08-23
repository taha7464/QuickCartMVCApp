using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuickKartDataAccessLayer;

using QuickKartMVCApp.Repositary;

namespace QuickKartMVCApp.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        QuickKartRepository rep;
        List<Models.Category> mCategory;
        MyMapper<Category, Models.Category> mapObj;
        MyMapper<Models.Category, Category> mapMod;
        List<Category> category;
            public CategoryController()
        {
            rep = new QuickKartRepository();
            mCategory = new List<Models.Category>();
            mapObj = new MyMapper<Category, Models.Category>();
            mapMod = new MyMapper<Models.Category, Category>();
            category = new List<Category>();

        }
        public ActionResult ViewCategory()
        {
           
          category = rep.GetCategoriesUsinglinq();
            foreach(var item in category)
            {
                mCategory.Add(mapObj.Translate(item));
            }
           
            return View(mCategory);
        }

     

        public ActionResult AddCategory()

        {
            
            
            return View();
        }
        public ActionResult SaveAddedCategory(Models.Category cat)
        {
           
            if (ModelState.IsValid)
            {
                try
                {
                    bool status = rep.AddCategory(mapMod.Translate(cat));
                    if (status)
                        return RedirectToAction("ViewCategory");
                    else
                        return View("Error");
                }
                catch (Exception ex)
                {
                    return View("Error");
                }
            }
            return View("AddCategory");
            
        }
        public ActionResult UpdateCategory(Models.Category cat)
        {
            return View(cat);
        }
        public ActionResult SaveUpdatedCategory(Models.Category cat)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    bool status = rep.UpdateCategory(mapMod.Translate(cat));
                    if (status)
                    {
                        return RedirectToAction("ViewCategory");
                    }
                    else
                    {
                        return View("Error");
                    }
                }
                catch(Exception ex)
                {
                    return View("Error");
                }
            }
            return View("UpdateCategory",cat);
        }

        public ActionResult DeleteCategory(Models.Category cat)
        {
            return View(cat);
        }
        public ActionResult SaveDeletedCategory(Models.Category cat)
        {
            try {
                
                    if (rep.DeleteCategory(mapMod.Translate(cat)))
                        return RedirectToAction("ViewCategory");
                    else
                        return View("Error");

                
            }
            catch(Exception ex)
            {
                return View("Error");
            }
        }
    }
}