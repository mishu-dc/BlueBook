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
    public class MarketHierarchyRepository:Repository<MarketHierarchy>, IMarketHierarchyRepository
    {
        public MarketHierarchyRepository(ApplicationDbContext dbContext):base(dbContext)
        {

        }

        private IQueryable<MarketHierarchy> ConstractQuery(string sortBy, string direction, string code, string name)
        {
            var query = Context.Set<MarketHierarchy>().Where(q => q.Id > 0);

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

        public async Task<List<MarketHierarchy>> GetMarketsAsync(string code, string name)
        {
            var query = ConstractQuery("name", "asc", code, name);
            return await query.ToListAsync();
        }
    }
}
