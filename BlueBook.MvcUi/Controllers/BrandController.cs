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
    public class BrandController : Controller
    {
        UnitOfWork _unitOfWork = null;

        //Initialize UnitOfWork using Ninject
        public BrandController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Brand
        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> List(int? page, int? limit, string sortBy, string direction, string code, string name)
        {
            try
            {
                List<Brand> records = await _unitOfWork.Brands.GetBrandsByPageAsync(page, limit, sortBy, direction, code, name);
                int total = await _unitOfWork.Brands.GetTotalBrandsAsync(page, limit, sortBy, direction, code, name);

                return this.Json(new { records, total }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return this.Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<JsonResult> Save(BrandModel record)
        {
            Brand brand = new Brand();

            if (ModelState.IsValid)
            {
                if (record.Id != null)
                {
                    brand = _unitOfWork.Brands.Get(record.Id.Value);
                    if (brand == null)
                    {
                        return Json(new { result = false }, JsonRequestBehavior.AllowGet);
                    }

                    brand.UpdatedBy = User.Identity.Name;
                    brand.UpdatedDate = DateTime.Now;
                }
                else
                {
                    brand.CreatedBy = User.Identity.Name;
                }

                brand.Code = record.Code;
                brand.Name = record.Name;
                if (record.Id == null)
                {
                    _unitOfWork.Brands.Add(brand);
                }

                await _unitOfWork.CompleteAsync();

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
            Brand brand = await _unitOfWork.Brands.GetAsync(id);

            if (brand == null)
            {
                return Json(new { result = false }, JsonRequestBehavior.AllowGet);
            }

            _unitOfWork.Brands.Remove(brand);
            await _unitOfWork.CompleteAsync();

            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        }
    }
}