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

        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        UnitOfWork _unitOfWork = null;

        public DistributorController(UnitOfWork unitOfWork)
        {
            logger.Debug("Distributor Controller is initialized with UnitOfWork");
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            logger.Debug("Distributor Controller Index action method is invoked");
            return View();
        }

        public async Task<JsonResult> ListAsync(int? page, int? limit, string sortBy, string direction, string code, string name)
        {
            try
            {
                DistributorModel model = new DistributorModel(_unitOfWork);

                List<Distributor> records = await model.GetDistributorsByPageAsync(page, limit, sortBy, direction, code, name);
                int total = await model.GetTotalDistributorsAsync(page, limit, sortBy, direction, code, name);

                logger.Debug("Distributor Controller List action method is invoked");
                logger.Debug(string.Format("Total {0} distributors are found and {1} is returned", total, records.Count()));

                return this.Json(new { records, total }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                logger.Error("Error while invoking List action method: ", ex);
                return this.Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
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
                    logger.Debug("Distributor saved successfully.");
                    return Json(new { result = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { result = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error while invoking Save action method: ", ex);
                return Json(new { result = false, error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<JsonResult> DeleteAsync(int id)
        {
            try
            {
                DistributorModel model = new DistributorModel(_unitOfWork);
                await model.DeleteAsync(id);
                logger.Debug("Distributor deleted successfully.");
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                logger.Error("Error while invoking Delete action method: ", ex);
                return Json(new { result = false, error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}