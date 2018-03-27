using BlueBook.DataAccess.Configurations;
using BlueBook.Entity.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Effort;

namespace BlueBook.MvcUi.Tests.Controllers
{
    public class BaseUnitTest
    {
        private IUnitOfWork _unitOfWork = null;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                if (_unitOfWork == null)
                {
                    ApplicationDbContext dbContext = new ApplicationDbContext(Effort.DbConnectionFactory.CreateTransient());
                    _unitOfWork = new UnitOfWork(dbContext);
                }
                return _unitOfWork;
            }
            
        }
    }
}
