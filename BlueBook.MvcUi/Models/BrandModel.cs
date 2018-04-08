using BlueBook.DataAccess.Entities;
using BlueBook.Entity.Configurations;
using BlueBook.MvcUi.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BlueBook.MvcUi.Models
{
    public class BrandModel
    {
        UnitOfWork _unitOfWork = null;

        public BrandModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Brand>> GetBrandsByPageAsync(int? page, int? limit, string sortBy, string direction, string code, string name)
        {
            return await _unitOfWork.Brands.GetBrandsByPageAsync(page, limit, sortBy, direction, code, name);
        }

        public async Task<int> GetTotalBrandsAsync(int? page, int? limit, string sortBy, string direction, string code, string name)
        {
            return await _unitOfWork.Brands.GetTotalBrandsAsync(page, limit, sortBy, direction, code, name);
        }

        public async Task<int> SaveAsync(BrandViewModel record)
        {
            Brand brand = null;

            if (record.Id != null)
            {
                brand = _unitOfWork.Brands.Get(record.Id.Value);
                if (brand == null)
                {
                    throw new Exception("Invalid Brand Id");
                }

                brand.UpdatedBy = System.Web.HttpContext.Current.User.Identity.Name;
                brand.UpdatedDate = DateTime.Now;
            }
            else
            {
                brand = new Brand();
                _unitOfWork.Brands.Add(brand);

                brand.CreatedBy = System.Web.HttpContext.Current.User.Identity.Name;
            }

            brand.Code = record.Code;
            brand.Name = record.Name;

            return await _unitOfWork.CompleteAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            Brand brand = await _unitOfWork.Brands.GetAsync(id);

            if (brand == null)
            {
                throw new Exception("Invalid Brand Id");
            }

            _unitOfWork.Brands.Remove(brand);
            return await _unitOfWork.CompleteAsync();
        }
    }
}