using BlueBook.DataAccess.Configurations;
using BlueBook.Entity.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBook.DataAccess.Tests
{
    public class BaseTest
    {
        public UnitOfWork UnitOfWork { get; set; }

        public BaseTest()
        {
            ApplicationDbContext context = new ApplicationDbContext(Effort.DbConnectionFactory.CreateTransient());
            UnitOfWork = new UnitOfWork(context);
        }
    }
}
