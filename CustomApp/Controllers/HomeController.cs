using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Script.Serialization;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CustomApp.Models;

namespace CustomApp.Controllers
{
    public class HomeController : Controller
    {
        private DemoEntities db = new DemoEntities();
        public object JsonConvert { get; private set; }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        //Get a List of Customers
        public JsonResult GetCustomerList()
        {
            List<CustomerViewModel> CustomerList = db.Customers.Select(x => new CustomerViewModel
            {
                Id = x.Id,
                CName = x.CName,
                Address = x.Address
            }).ToList();
            return Json(CustomerList, JsonRequestBehavior.AllowGet);
        }

        //To Save data in database either on creating or editing Customer
        public JsonResult SaveDataInDatabase(CustomerViewModel model)
        {
            var result = false;
            try
            {
                //Save data on Editing the customer
                if (model.Id > 0)
                {
                    Customer Cus = db.Customers.SingleOrDefault(x => x.Id == model.Id);
                    Cus.CName = model.CName;
                    Cus.Address = model.Address;
                    db.SaveChanges();
                    result = true;
                }
                //Save data on Adding the customer
                else
                {
                    Customer Cus = new Customer();
                    Cus.Id = model.Id;
                    Cus.CName = model.CName;
                    Cus.Address = model.Address;
                    db.Customers.Add(Cus);
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

        //Return Customer Details based on Id 
        public JsonResult GetCustomerById(int id)
        {
            Customer cus = db.Customers.SingleOrDefault(x => x.Id == id);
            Customer cr = new Customer();
            cr.Id = cus.Id;
            cr.CName = cus.CName;
            cr.Address = cus.Address;

            var serializer = new JavaScriptSerializer();
            var output = serializer.Serialize(cr);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        //Delete the customer data
        public JsonResult CustomerDeleteRecord(int Id)
        {
            bool result = false;
            Customer Cus = db.Customers.SingleOrDefault(x => x.Id == Id);
            if (Cus != null)
            {
                db.Customers.Remove(Cus);
                db.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
