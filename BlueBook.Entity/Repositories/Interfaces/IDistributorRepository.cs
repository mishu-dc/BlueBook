using BlueBook.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBook.Entity.Repositories.Interfaces
{
    public interface IDistributorRepository:IRepository<Distributor>
    {
        Task<List<Distributor>> GetDistributorsAsync(string code, string name);
        Task<List<Distributor>> GetDistributorsByPageAsync(int? page, int? limit, string sortBy, string direction, string code, string name);
        Task<int> GetTotalDistributorsAsync(int? page, int? limit, string sortBy, string direction, string code, string name);
    }
}
