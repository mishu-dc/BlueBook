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
    public class FieldForceController : Controller
    {
        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        UnitOfWork _unitOfWork = null;

        public FieldForceController(UnitOfWork unitOfWork)
        {
            logger.Debug("Fieldforce Controller is initialized with UnitOfWork");
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            logger.Debug("Fieldforce Controller Index action method is invoked");
            return View();
        }

        public async Task<JsonResult> ListAsync(int? page, int? limit, string sortBy, string direction, string code, string name, int? distributorId)
        {
            try
            {
                FieldForceModel model = new FieldForceModel(_unitOfWork);

                List<FieldForce> records = await model.GetFieldForceByPageAsync(page, limit, sortBy, direction, code, name, distributorId);
                int total = await model.GetTotalFieldForceAsync(page, limit, sortBy, direction, code, name, distributorId);

                logger.Debug("Fieldforce Controller List action method is invoked");
                logger.Debug(string.Format("Total {0} fieldforces are found and {1} is returned", total, records.Count()));

                return this.Json(new
                {
                    records = records.Select(f => new FieldForceViewModel()
                    {
                        AddressLine1 = f.Address != null ? f.Address.AddressLine1 : string.Empty,
                        AddressLine2 = f.Address != null ? f.Address.AddressLine2 : string.Empty,
                        City = f.Address != null ? f.Address.City : string.Empty,
                        State = f.Address != null ? f.Address.State : string.Empty,
                        Zip = f.Address != null ? f.Address.Zip : string.Empty,
                        Code = f.Code,
                        Email = f.Email,
                        Id = f.Id,
                        MarketHierarchyId = f.MarketHierarchy != null ? f.MarketHierarchy.Id : 0,
                        Name = f.Name,
                        Phone = f.Phone,
                        DistributorIds = f.Distributors.Select(x => x.Id).ToList<int>()
                    }),
                    total
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                logger.Error("Error while invoking List action method: ", ex);
                return this.Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<JsonResult> SaveAsync(FieldForceViewModel record)
        {
            try
            {
                FieldForceModel model = new FieldForceModel(_unitOfWork);
                if (ModelState.IsValid)
                {
                    await model.SaveAsync(record);
                    logger.Debug("Fieldforce saved successfully.");
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
                FieldForceModel model = new FieldForceModel(_unitOfWork);
                await model.DeleteAsync(id);
                logger.Debug("Fieldforce deleted successfully.");
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