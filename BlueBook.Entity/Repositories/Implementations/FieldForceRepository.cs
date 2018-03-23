using BlueBook.DataAccess.Configurations;
using BlueBook.DataAccess.Entities;
using BlueBook.Entity.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
    }
}
