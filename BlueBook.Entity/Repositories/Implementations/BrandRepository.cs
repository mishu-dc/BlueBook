using BlueBook.DataAccess.Configurations;
using BlueBook.DataAccess.Entities;
using BlueBook.Entity.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBook.Entity.Repositories.Implementations
{
    public class BrandRepository:Repository<Brand>, IBrandRepository
    {
        public BrandRepository(ApplicationDbContext dbContext):base(dbContext)
        {

        }
    }
}
