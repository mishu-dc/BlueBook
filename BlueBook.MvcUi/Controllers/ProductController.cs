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
    public class ProductController : Controller
    {
        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        UnitOfWork _unitOfWork = null;

        //Initialize UnitOfWork using Ninject
        public ProductController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            logger.Debug("Product Controller Index action method is invoked");
            return View();
        }

        // GET: Product
        public async Task<JsonResult> ListAsync(int? page, int? limit, string sortBy, string direction, string code, string name, int? brandId)
        {
            try
            {
                ProductModel model = new ProductModel(_unitOfWork);
                var records = await model.GetProductsByPageAsync(page, limit, sortBy, direction, code, name, brandId);
                int total = await model.GetTotalBrandsAsync(page, limit, sortBy, direction, code, name, brandId);

                logger.Debug("Product Controller List action method is invoked");
                logger.Debug(string.Format("Total {0} brands are found and {1} is returned", total, records.Count()));

                return this.Json(new { records = records.Select(x => new ProductViewModel() { Id = x.Id, BrandId = x.BrandId, BrandName = x.Brand != null ? x.Brand.Name : string.Empty, Name = x.Name, Code = x.Code, Price = x.Price }), total }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                logger.Error("Error while invoking List action method: ", ex);
                return this.Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<JsonResult> SaveAsync(ProductViewModel record)
        {
            try
            {
                Product product = new Product();

                if (ModelState.IsValid)
                {
                    ProductModel model = new ProductModel(_unitOfWork);
                    await model.SaveAsync(record);

                    logger.Debug("Product saved successfully.");

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
                ProductModel model = new ProductModel(_unitOfWork);
                await model.DeleteAsync(id);
                logger.Debug("Product deleted successfully.");
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