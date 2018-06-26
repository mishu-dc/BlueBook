using BlueBook.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBook.Entity.Repositories.Interfaces
{
    public interface IMarketHierarchyRepository:IRepository<MarketHierarchy>
    {
        Task<List<MarketHierarchy>> GetMarketsAsync(string code, string name);
    }
}
