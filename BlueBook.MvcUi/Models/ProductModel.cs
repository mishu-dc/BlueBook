using BlueBook.DataAccess.Entities;
using BlueBook.Entity.Configurations;
using BlueBook.MvcUi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BlueBook.MvcUi.Models
{
    public class ProductModel
    {
        UnitOfWork _unitOfWork = null;

        public ProductModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Product>> GetProductsByPageAsync(int? page, int? limit, string sortBy, string direction, string code, string name, int? brandId)
        {
            return await _unitOfWork.Products.GetProductsByPageAsync(page, limit, sortBy, direction, code, name, brandId);
        }

        public async Task<int> GetTotalBrandsAsync(int? page, int? limit, string sortBy, string direction, string code, string name, int? brandId)
        {
            return await _unitOfWork.Products.GetTotalProductsAsync(page, limit, sortBy, direction, code, name, brandId);
        }

        public async Task<int> SaveAsync(ProductViewModel record)
        {
            Brand brand = null;
            Product product = null;

            if (record.Id != null)
            {
                product = _unitOfWork.Products.Get(record.Id.Value);
                if (product == null)
                {
                    throw new Exception("Invalid Product Id");
                }

                product.UpdatedBy = System.Web.HttpContext.Current.User.Identity.Name;
                product.UpdatedDate = DateTime.Now;
            }
            else
            {
                product = new Product();
                _unitOfWork.Products.Add(product);
                product.CreatedBy = System.Web.HttpContext.Current.User.Identity.Name;
            }

            brand = _unitOfWork.Brands.Get(record.BrandId);

            if(brand==null)
            {
                throw new Exception("Invalid Brand Id");
            }

            product.Brand = brand;
            product.Code = record.Code;
            product.Name = record.Name;
            product.Price = record.Price.Value;

            return await _unitOfWork.CompleteAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            Product product = await _unitOfWork.Products.GetAsync(id);

            if (product == null)
            {
                throw new Exception("Invalid Product Id");
            }

            _unitOfWork.Products.Remove(product);
            return await _unitOfWork.CompleteAsync();
        }
    }
}