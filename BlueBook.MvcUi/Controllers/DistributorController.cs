using BlueBook.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueBook.MvcUi.Controllers
{
    public class DistributorController : Controller
    {
        // GET: Distributor
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult List()
        {
            List<Distributor> distributors = new List<Distributor>();
            distributors.Add(new Distributor() { Code = "D1", Name = "Dhaka", Address = "Uttora, Dhaka" });
            distributors.Add(new Distributor() { Code = "D2", Name = "Chittagong", Address = "Uttora, Chittagong" });
            distributors.Add(new Distributor() { Code = "D3", Name = "Khulna", Address = "Uttora, Khulna" });
            distributors.Add(new Distributor() { Code = "D4", Name = "Borishal", Address = "Uttora, Borishal" });

            return Json(distributors, JsonRequestBehavior.AllowGet);
        }
    }
}