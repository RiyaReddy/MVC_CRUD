using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Script.Serialization;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using CustomApp.Models;

namespace CustomApp.Controllers
{
    public class SalesController : Controller
    {
        private DemoEntities db = new DemoEntities();

        public ActionResult Index()
        {
            List<Product> plist = db.Products.ToList();
            ViewBag.ProductId = new SelectList(plist, "Id", "PName");
            List<Customer> clist = db.Customers.ToList();
            ViewBag.ListOfCustomer = new SelectList(clist, "Id", "CName");
            List<Store> slist = db.Stores.ToList();
            ViewBag.ListOfStore = new SelectList(slist, "Id", "SName");
            return View();
        }
        // GET: Return List of Sales
        public JsonResult GetList()
        {
            List<SalesViewModel> List = db.ProductSolds.Select(x => new SalesViewModel
            {
                Id = x.Id,                
                CName = x.Customer.CName,
                PName = x.Product.PName,
                SName = x.Store.SName,
                DateSold = x.DateSold
            }).ToList();
                        
            return Json(List,JsonRequestBehavior.AllowGet);
        }
    
        //Return data based on particular Id
        public JsonResult Edit(int id)
        {

            ProductSold productSold = db.ProductSolds.Find(id);
                        
            string value = string.Empty;
            value = JsonConvert.SerializeObject(productSold,
                Formatting.Indented,
                new JsonSerializerSettings {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            return Json(value, JsonRequestBehavior.AllowGet);

        }

        //Save data in database either on creating or editing Sales
        public JsonResult Create(ProductSold model)
        {
            var result = false;
            try
            {
                if (model.Id > 0)      //Updating
                {
                    ProductSold pro = db.ProductSolds.SingleOrDefault(x => x.Id == model.Id);

                    if (model.ProductId == 0)   //Changes not made for this field
                    {
                        pro.ProductId = pro.ProductId;
                    }
                    else
                    {
                        pro.ProductId = model.ProductId;
                    }
                    if(model.CustomerId == 0)
                    {

                        pro.CustomerId = pro.CustomerId;
                    }
                    else
                    {

                        pro.CustomerId = model.CustomerId;
                    }
                    if (model.StoreId == 0)
                    {

                        pro.StoreId = pro.StoreId;
                    }
                    else
                    {

                        pro.StoreId = model.StoreId;
                    }
                     pro.DateSold = model.DateSold;                   
                     db.Entry(pro).State = EntityState.Modified;
                     db.SaveChanges();
                     result = true;
                    
                }
                else     //Adding
                {
                    ProductSold pro = new ProductSold();
                    pro.ProductId = model.ProductId;
                    pro.CustomerId = model.CustomerId;
                    pro.StoreId = model.StoreId;
                    pro.DateSold = model.DateSold;
                    db.ProductSolds.Add(pro);                    
                    db.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int id)
        {
            bool result = false;
            ProductSold pro = db.ProductSolds.SingleOrDefault(x => x.Id == id);
            if (pro != null)
            {
                db.ProductSolds.Remove(pro);
                db.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }

}
