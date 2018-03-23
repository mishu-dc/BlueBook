using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueBook.DataAccess.Configurations;
using BlueBook.Entity.Repositories.Implementations;
using BlueBook.Entity.Repositories.Interfaces;

namespace BlueBook.Entity.Configurations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _context = dbContext;

            FieldForces = new FieldForceRepository(dbContext);
            Brands = new BrandRepository(dbContext);
            Products = new ProductRepository(dbContext);
            MarketHierarchies = new MarketHierarchyRepository(dbContext);
            Distributors = new DistributorRepository(dbContext);
        }

        public IFieldForceRepository FieldForces
        {
            get; private set;
        }

        public IBrandRepository Brands
        {
            get; private set;
        }

        public IProductRepository Products
        {
            get; private set;
        }

        public IMarketHierarchyRepository MarketHierarchies
        {
            get; private set;
        }

        public IDistributorRepository Distributors
        {
            get; private set;
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
           return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
