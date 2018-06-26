using BlueBook.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBook.Entity.Repositories.Interfaces
{
    public interface IProductRepository:IRepository<Product>
    {
        Task<Product> GetProductAsync(int id);
        Task<List<Product>> GetProductsAsync(string code, string name, int brandId);
        Task<List<Product>> GetProductsByPageAsync(int? page, int? limit, string sortBy, string direction, string code, string name, int? brandId);
        Task<int> GetTotalProductsAsync(int? page, int? limit, string sortBy, string direction, string code, string name, int? brandId);
    }
}
