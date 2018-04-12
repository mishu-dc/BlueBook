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
    public class MarketHierarchyController : Controller
    {
        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        UnitOfWork _unitOfWork = null;

        //Initialize UnitOfWork using Ninject
        public MarketHierarchyController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: MarketHierarchy
        public ActionResult Index()
        {
            logger.Debug("Brand Controller Index action method is invoked");
            return View();
        }

        public async Task<JsonResult> ListAsync()
        {
            try
            {
                logger.Debug("MarketHierarchy Controller List action method is invoked");

                MarketHierarchyModel model = new MarketHierarchyModel(_unitOfWork);

                var records = await model.GetMarketHierarchyAsync();

                return this.Json(records , JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                logger.Error("Error while invoking List action method: ", ex);
                return this.Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<JsonResult> SaveAsync(MarketHierarchyViewModel record)
        {
            try
            {
                MarketHierarchy mh = null;

                if (ModelState.IsValid)
                {
                    MarketHierarchyModel model = new MarketHierarchyModel(_unitOfWork);
                    mh = await model.SaveAsync(record);

                    logger.Debug("Market Hierarchy saved successfully.");

                    return Json(new MarketHierarchyTreeViewModel() { text = mh.Code + "-" + mh.Name, code = mh.Code, id = mh.Id, name = mh.Name, parentId = mh.ParentId!=null?mh.ParentId.Value:-1, type = mh.Type.ToString() } , JsonRequestBehavior.AllowGet);
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
        public async Task<JsonResult> DeleteAsync(int Id)
        {
            try
            {
                MarketHierarchyModel model = new MarketHierarchyModel(_unitOfWork);
                await model.DeleteAsync(Id);
                logger.Debug("Brand deleted successfully.");
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