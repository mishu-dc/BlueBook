using BlueBook.DataAccess.Entities;
using BlueBook.Entity.Configurations;
using BlueBook.MvcUi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BlueBook.MvcUi.Controllers
{
    [Authorize]
    public class SearchController : Controller
    {
        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        UnitOfWork _unitOfWork = null;

        //Initialize UnitOfWork using Ninject
        public SearchController(UnitOfWork unitOfWork)
        {
            logger.Debug("Search Controller is initialized with UnitOfWork");
            _unitOfWork = unitOfWork;
        }

        public async Task<JsonResult> GetBrands()
        {
            try
            {
                logger.Debug("Search Controller GetBrands action method is invoked");

                BrandModel model = new BrandModel(_unitOfWork);

                List<Brand> records = await model.GetBrandsByPageAsync(null, null, string.Empty, string.Empty, string.Empty, string.Empty);
                
                return this.Json(records.Select(x=> new { Id = x.Id, Name = x.Name }) , JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                logger.Error("Error while invoking GetBrands action method: ", ex);
                return this.Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<JsonResult> GetDistributors()
        {
            try
            {
                logger.Debug("Search Controller GetDistributors action method is invoked");

                DistributorModel model = new DistributorModel(_unitOfWork);

                List<Distributor> records = await model.GetDistributorsByPageAsync(null, null, string.Empty, string.Empty, string.Empty, string.Empty);

                return this.Json(records.Select(x => new { Id = x.Id, Name = x.Name }), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                logger.Error("Error while invoking GetDistributors action method: ", ex);
                return this.Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}