using BlueBook.DataAccess.Configurations;
using BlueBook.DataAccess.Entities;
using BlueBook.Entity.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBook.Entity.Repositories.Implementations
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        private IQueryable<Product> ConstractQuery(string sortBy, string direction, string code, string name, int? brandId)
        {
            var query = Context.Set<Product>().Where(q => q.Id > 0);

            if (!string.IsNullOrWhiteSpace(code))
            {
                query = query.Where(q => q.Code != null && q.Code.Contains(code));
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(q => q.Name.Contains(name));
            }

            if (brandId != null && brandId.Value>0)
            {
                query = query.Where(q => q.BrandId == brandId.Value);
            }

            if (!string.IsNullOrEmpty(sortBy) && !string.IsNullOrEmpty(direction))
            {
                if (direction.Trim().ToLower() == "asc")
                {
                    switch (sortBy.Trim().ToLower())
                    {
                        case "code":
                            query = query.OrderBy(q => q.Code);
                            break;
                        case "name":
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                }
                else
                {
                    switch (sortBy.Trim().ToLower())
                    {
                        case "code":
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case "name":
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                }
            }
            else
            {
                query = query.OrderBy(q => q.Name);
            }

            return query;
        }

        public async Task<List<Product>> GetProductsAsync(string code, string name, int brandId)
        {
            var query = ConstractQuery("name", "asc", code, name, brandId);
            var products = await query.Include(p=>p.Brand).ToListAsync();
            return products;
        }

        public async Task<List<Product>> GetProductsByPageAsync(int? page, int? limit, string sortBy, string direction, string code, string name, int? brandId)
        {
            var query = ConstractQuery(sortBy, direction, code, name, brandId);

            if (page.HasValue && limit.HasValue)
            {
                int start = (page.Value - 1) * limit.Value;
                query = query.Skip(start).Take(limit.Value);
            }
            
            return await query.Include(x=>x.Brand).ToListAsync();
        }

        public async Task<int> GetTotalProductsAsync(int? page, int? limit, string sortBy, string direction, string code, string name, int? brandId)
        {
            var query = ConstractQuery(sortBy, direction, code, name, brandId);

            return await query.CountAsync();
        }

        public async Task<Product> GetProductAsync(int id)
        {
            Product product = await Context.Set<Product>().FindAsync(id);
            return product;
        }
    }
}
