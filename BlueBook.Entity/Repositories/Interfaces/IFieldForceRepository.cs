using BlueBook.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBook.Entity.Repositories.Interfaces
{
    public interface IFieldForceRepository : IRepository<FieldForce>
    {
        IEnumerable<FieldForce> GetFieldForceByMarketHierarchyId(int marketHierarchyId);
        IEnumerable<FieldForce> GetFieldForceByDistributorId(int distributorId);
        IEnumerable<FieldForce> GetFieldForceByDistributors(List<Distributor> distributors);
        IEnumerable<FieldForce> GetFieldForceByDistributorIds(List<int> distributorIds);
    }
}
