using BlueBook.DataAccess.Configurations;
using BlueBook.DataAccess.Entities;
using BlueBook.Entity.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BlueBook.Entity.Repositories.Implementations
{
    public class FieldForceRepository : Repository<FieldForce>, IFieldForceRepository
    {
        public FieldForceRepository(ApplicationDbContext context) : base(context)
        {

        }

        public IEnumerable<FieldForce> GetFieldForceByDistributorId(int distributorId)
        {
            return Context.Set<FieldForce>().Where(x => x.Distributors.Any(d => d.Id == distributorId)).ToList();
        }

        public IEnumerable<FieldForce> GetFieldForceByDistributorIds(List<int> distributorIds)
        {
            return Context.Set<FieldForce>().Where(x => x.Distributors.All(d => distributorIds.Contains(d.Id))).ToList();
        }

        public IEnumerable<FieldForce> GetFieldForceByDistributors(List<Distributor> distributors)
        {
            List<FieldForce> ffs = new List<FieldForce>();

            foreach(Distributor dist in distributors)
            {
                ffs.AddRange(Context.Set<FieldForce>().Where(x => x.Distributors.Contains(dist)).ToList());
            }

            return ffs.Distinct();
        }

        public IEnumerable<FieldForce> GetFieldForceByMarketHierarchyId(int marketHierarchyId)
        {
            return Context.Set<FieldForce>().Where(x => x.MarketHierarchyId == marketHierarchyId).ToList();
        }

        private IQueryable<FieldForce> ConstractQuery(string sortBy, string direction, string code, string name, int? distributorId)
        {
            var query = Context.Set<FieldForce>().Where(q => q.Id > 0);

            if (!string.IsNullOrWhiteSpace(code))
            {
                query = query.Where(q => q.Code != null && q.Code.Contains(code));
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(q => q.Name.Contains(name));
            }

            if (distributorId != null && distributorId.Value>0)
            {
                query = query.Where(q => q.Distributors.Any(d => d.Id == distributorId.Value));
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

        public async Task<int> GetTotalFieldForceAsync(int? page, int? limit, string sortBy, string direction, string code, string name, int? distributorId)
        {
            var query = ConstractQuery(sortBy, direction, code, name, distributorId);

            return await query.Include(f=>f.Distributors).Include(f=>f.MarketHierarchy).CountAsync();
        }

        public async Task<List<FieldForce>> GetFieldForceByPageAsync(int? page, int? limit, string sortBy, string direction, string code, string name, int? distributorId)
        {
            var query = ConstractQuery(sortBy, direction, code, name, distributorId);

            if (page.HasValue && limit.HasValue)
            {
                int start = (page.Value - 1) * limit.Value;
                query = query.Skip(start).Take(limit.Value);
            }

            return await query.ToListAsync();
        }
    }
}
