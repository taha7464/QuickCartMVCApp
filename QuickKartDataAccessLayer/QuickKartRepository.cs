using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickKartDataAccessLayer
{
    public class QuickKartRepository
    {
        QuickKartDBContext context;
        public QuickKartRepository()
        {
            context = new QuickKartDBContext();
        }

        public List<Category>GetCategoriesUsinglinq()
        {
            return context.Categories.ToList<Category>();
        }

        public bool AddCategory(Category catObj)
        {
            try
            {
                context.Categories.Add(catObj);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateCategory(Category catObj)
        {
            try
            {
                var catObjFromDB = context.Categories.Where(x => x.CategoryId == catObj.CategoryId).Select(x => x).FirstOrDefault();
                catObjFromDB.CategoryName = catObj.CategoryName;
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteCategory(Category catObj)
        {
            try
            {
                var categoryToBeDeleted = context.Categories.Where(x => x.CategoryId == catObj.CategoryId).FirstOrDefault();
                context.Categories.Remove(categoryToBeDeleted);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public byte? GetRoleIdByUserId(string userId)
        {
            try
            {
                var roleId = context.Users.Where(x => x.EmailId == userId).Select(x => x.RoleId).FirstOrDefault();
                //var roleName = context.Roles.Where(x => x.RoleId == roleId).Select(x => x.RoleName).FirstOrDefault();
                return roleId;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public string GetRoleName(int roleId)
        {
            try
            {                
                var roleName = context.Roles.Where(x => x.RoleId == roleId).Select(x => x.RoleName).FirstOrDefault();
                return roleName;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Product>GetProductUsingLinq()
        {
            return context.Products.ToList<Product>();
        }

        public string GetNewProductIdUsingUFN()
        {
            string productid = "";
            try
            {
                // SQL server specific usage
                productid = context.Database.SqlQuery<string>("SELECT dbo.ufn_GenerateNewProductId()").FirstOrDefault();
                //Provider independant usage
                //productid = ((IObjectContextAdapter)Context).ObjectContext.ExecuteStoreQuery<string>("SELECT dbo.ufn_GenerateNewProductId()").FirstOrDefault();
            }
            catch (Exception)
            {
                productid = "";
            }
            return productid;
        }
        public bool AddProduct(Product prodObj)
        {
            
            try
            {
                context.Products.Add(prodObj);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public bool UpdateProduct(Product prodObj)
        {
            try
            {
                var prodObjFromDB = context.Products.Where(x => x.ProductId == prodObj.ProductId).Select(x => x).FirstOrDefault();

                prodObjFromDB.ProductName = prodObj.ProductName;
                prodObjFromDB.CategoryId = prodObj.CategoryId;
                prodObjFromDB.QuantityAvailable = prodObj.QuantityAvailable;
                prodObjFromDB.Price = prodObj.Price;
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteProduct(Product catObj)
        {
            try
            {
                var productToBeDeleted = context.Products.Where(x => x.ProductId == catObj.ProductId).FirstOrDefault();
                context.Products.Remove(productToBeDeleted);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool PurchaseProduct(PurchaseDetail purchaseObj)
        {
            try
            {
                purchaseObj.DateOfPurchase = DateTime.Now;
                context.PurchaseDetails.Add(purchaseObj);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<string> FetchTopProducts()
        {
            List<string> productList = new List<string>();
            try
            {
                var list = (from c in context.PurchaseDetails
                            join p in context.Products on
                            c.ProductId equals p.ProductId
                            select p.ProductName).Distinct().ToList<string>();
                if (list.Count > 3)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        productList.Add(list[i]);
                    }
                }
                else
                {
                    productList = list;
                }

            }
            catch
            {

                productList = null;
            }
            return productList;
        }

        public List<string> FetchTopProducts(string emailId)
        {
            List<string> productList = new List<string>();
            try
            {
                var list = (from c in context.PurchaseDetails
                            join p in context.Products on
                            c.ProductId equals p.ProductId
                            where c.EmailId == emailId
                            select p.ProductName).Distinct().ToList<string>();
                if (list.Count > 3)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        productList.Add(list[i]);
                    }
                }
                else
                {
                    productList = list;
                }

            }
            catch
            {

                productList = null;
            }
            return productList;
        }

	public bool RegisterUser(string emailId, string password, string gender, DateTime dateOfBirth, string address)
        {
            bool status = false;
            try
            {
                var role = (from r in context.Roles
                            where r.RoleName.Equals("Customer")
                            select r).SingleOrDefault<Role>();
                if(role!=null)
                {
                    User user = new User();
                    user.EmailId = emailId;
                    user.UserPassword = password;
                    user.Gender = gender;
                    user.DateOfBirth = dateOfBirth;
                    user.Address = address;
                    user.Role = role;

                    context.Users.Add(user);
                    context.SaveChanges();
                    status = true;
                }
            }
            catch(Exception ex)
            {
                status = false;
            }
            return status;
        }


        public bool ValidateCredentials(string userName,string password)
        {
            try
            {
                var validUser = context.Users.Where(x => x.EmailId == userName && x.UserPassword == password).Select(x => x).FirstOrDefault();
                if(validUser!=null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    


    }
}
