using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using QuickKartDataAccessLayer;
using QuickKartWebService.Repository;

namespace QuickKartWebService.Controllers
{
    public class ProductController : ApiController
    {
        [HttpGet]
        public JsonResult<List<Models.Product>> GetProducts()
        {
            try
            {
                var mapObj = new QuickKartMapper<Product, Models.Product>();
                var dal = new QuickKartRepository();
                var productList = dal.GetProductUsingLinq();
                var products = new List<Models.Product>();
                if (productList.Any())
                {
                    foreach (var product in productList)
                    {
                        products.Add(mapObj.Translate(product));
                    }
                }
                return Json<List<Models.Product>>(products);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public JsonResult<Models.Product> GetProduct(string id)
        {
            try
            {
                var mapObj = new QuickKartMapper<Product, Models.Product>();
                var dal = new QuickKartRepository();
                 Product productList = dal.GetProductUsingLinq().Where(x=>x.ProductId== id).FirstOrDefault();
              
              
                       
                
                return Json<Models.Product> (mapObj.Translate(productList));
            }
            catch (Exception ex)
            {
                return null;
            }
        }



    }

}
