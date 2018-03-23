using BlueBook.Entity.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBook.Entity.Configurations
{
    public interface IUnitOfWork : IDisposable
    {
        IFieldForceRepository FieldForces { get; }
        IBrandRepository Brands { get; }
        IProductRepository Products { get; }
        IMarketHierarchyRepository MarketHierarchies { get; }
        IDistributorRepository Distributors { get; }
        int Complete();
        Task<int> CompleteAsync();
    }
}
