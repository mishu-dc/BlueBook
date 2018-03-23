using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueBook.DataAccess.Entities;

namespace BlueBook.DataAccess.Mappings
{
    public class DistributorMapping:EntityMapping<Distributor>
    {
        public DistributorMapping()
        {
            Property(x => x.Address).IsRequired().HasMaxLength(500);
            Property(x => x.Code).IsRequired().HasMaxLength(25);
            Property(x => x.Name).IsRequired().HasMaxLength(255);
        }
    }
}
