using BlueBook.DataAccess.Entities;
using BlueBook.Entity.Configurations;
using BlueBook.MvcUi.Models;
using BlueBook.MvcUi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BlueBook.MvcUi.Controllers
{
    public class DistributorController : Controller
    {
        UnitOfWork _unitOfWork = null;

        public DistributorController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> ListAsync(int? page, int? limit, string sortBy, string direction, string code, string name)
        {
            try
            {
                DistributorModel model = new DistributorModel(_unitOfWork);

                List<Distributor> records = await model.GetDistributorsByPageAsync(page, limit, sortBy, direction, code, name);
                int total = await model.GetTotalDistributorsAsync(page, limit, sortBy, direction, code, name);

                return this.Json(new { records, total }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return this.Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<JsonResult> SaveAsync(DistributorViewModel record)
        {
            try
            {
                DistributorModel model = new DistributorModel(_unitOfWork);
                if (ModelState.IsValid)
                {
                    await model.SaveAsync(record);
                    return Json(new { result = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { result = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception ex)
            {
                return Json(new { result = false, ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<JsonResult> DeleteAsync(int id)
        {
            try
            {
                DistributorModel model = new DistributorModel(_unitOfWork);
                await model.DeleteAsync(id);
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}