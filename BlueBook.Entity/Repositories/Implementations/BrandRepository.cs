using BlueBook.DataAccess.Configurations;
using BlueBook.DataAccess.Entities;
using BlueBook.Entity.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlueBook.Entity.Repositories.Implementations
{
    public class BrandRepository:Repository<Brand>, IBrandRepository
    {
        public BrandRepository(ApplicationDbContext dbContext):base(dbContext)
        {

        }
        
        private IQueryable<Brand> ConstractQuery(string sortBy, string direction, string code, string name)
        {
            var query = Context.Set<Brand>().Where(q => q.Id > 0);

            if (!string.IsNullOrWhiteSpace(code))
            {
                query = query.Where(q => q.Code != null && q.Code.Contains(code));
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(q => q.Name.Contains(name));
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

        public async Task<int> GetTotalBrandsAsync(int? page, int? limit, string sortBy, string direction, string code, string name)
        {
            var query = ConstractQuery(sortBy, direction, code, name);

            return await query.CountAsync();
        }

        public async Task<List<Brand>> GetBrandsAsync(string code, string name)
        {
            var query = ConstractQuery("name", "asc", code, name);
            return await query.ToListAsync();
        }

        public async Task<List<Brand>> GetBrandsByPageAsync(int? page, int? limit, string sortBy, string direction, string code, string name)
        {
            var query = ConstractQuery(sortBy, direction, code, name);

            if (page.HasValue && limit.HasValue)
            {
                int start = (page.Value - 1) * limit.Value;
                query = query.Skip(start).Take(limit.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<List<Brand>> GetBrandsAsync()
        {
            var query = ConstractQuery(string.Empty, string.Empty,string.Empty, string.Empty);
            
            return await query.ToListAsync();
        }

        public async Task<Brand> GetBrandAsync(int Id)
        {
            Brand brand = await Context.Set<Brand>().FindAsync(Id);
            return brand;
        }

       
    }
}
