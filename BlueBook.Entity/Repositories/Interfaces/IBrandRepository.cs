using BlueBook.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBook.Entity.Repositories.Interfaces
{
    public interface IBrandRepository : IRepository<Brand>
    {
        Task<List<Brand>> GetBrandsByPageAsync(int? page, int? limit, string sortBy, string direction, string code, string name);
        Task<int> GetTotalBrandsAsync(int? page, int? limit, string sortBy, string direction, string code, string name);
    }
}
