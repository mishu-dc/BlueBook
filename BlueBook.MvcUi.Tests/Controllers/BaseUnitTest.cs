using BlueBook.DataAccess.Configurations;
using BlueBook.Entity.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    _unitOfWork = new UnitOfWork(new ApplicationDbContext());
                }
                return _unitOfWork;
            }
            
        }
    }
}
