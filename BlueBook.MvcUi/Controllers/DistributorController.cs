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

        public async Task<JsonResult> List(int? page, int? limit, string sortBy, string direction, string code, string name)
        {
            try
            {
                List<Distributor> records = await _unitOfWork.Distributors.GetDistributorsByPageAsync(page, limit, sortBy, direction, code, name);
                int total = await _unitOfWork.Distributors.GetTotalDistributorsAsync(page, limit, sortBy, direction, code, name);

                return this.Json(new { records, total }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return this.Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Save(DistributorModel record)
        {
            Distributor distributor = new Distributor();

            if (ModelState.IsValid)
            {
                if (record.Id!=null)
                {
                    distributor = _unitOfWork.Distributors.Get(record.Id.Value);
                    distributor.UpdatedBy = User.Identity.Name;
                    distributor.UpdatedDate = DateTime.Now;
                    if (distributor == null)
                    {
                        return Json(new { result = false }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    distributor.CreatedBy = User.Identity.Name;
                }

                distributor.Code = record.Code;
                distributor.Name = record.Name;
                distributor.Address = record.Address;


                _unitOfWork.Distributors.Add(distributor);

                 _unitOfWork.Complete();

                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            Distributor distributor = await _unitOfWork.Distributors.GetAsync(id);

            if (distributor == null)
            {
                return Json(new { result = false }, JsonRequestBehavior.AllowGet);
            }

            _unitOfWork.Distributors.Remove(distributor);
            await _unitOfWork.CompleteAsync();

            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        }
    }
}