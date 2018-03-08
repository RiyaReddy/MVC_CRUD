using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Linq;
using System.Net;
using System.Web;

using System.Web.Mvc;
using CustomApp.Models;

namespace CustomApp.Controllers
{
    public class ProductsController : Controller
    {
        private DemoEntities db = new DemoEntities();

        public object JsonConvert { get; private set; }

        public ActionResult Index()
        {
            return View();
        }
        
        //Return list of Products
        public JsonResult Getlist()
        {
            List<ProductViewModel> PList = db.Products.Select(x => new ProductViewModel
            {
                Id = x.Id,
                PName = x.PName,
                Price = x.Price
            }).ToList();

            return Json(PList,JsonRequestBehavior.AllowGet);
        }

         //Save the data in database either on creating or editing Product     
        public JsonResult Create(ProductViewModel model)
        {
            var result = false;

            try
            {

                Product pro = new Product();
                pro.Id = model.Id;
                pro.PName = model.PName;
                pro.Price = model.Price;
                if (model.Id == 0)
                {
                    
                    db.Products.Add(pro);
                    db.SaveChanges();
                    result = true;
                }
                else
                {                               
                    db.Entry(pro).State = EntityState.Modified;
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
        
        //Return respective Product Data based on Id
        public JsonResult Edit(int id)
        {
            Product pro = db.Products.SingleOrDefault(x => x.Id == id);
            Product pr = new Product();
            pr.Id = pro.Id;
            pr.PName = pro.PName;
            pr.Price = pro.Price;
            var serializer = new JavaScriptSerializer();
            var output = serializer.Serialize(pr);
            return Json(output,JsonRequestBehavior.AllowGet);
        }
        
        //Delete a Product based on its Id
        public JsonResult Delete(int id)
        {
            bool result = false;
            Product pro = db.Products.SingleOrDefault(x => x.Id == id);
      
            if (pro != null)
            {
                db.Products.Remove(pro);
                db.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

       
    }
}
