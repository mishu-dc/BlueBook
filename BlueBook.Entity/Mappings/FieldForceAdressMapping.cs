using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueBook.DataAccess.Entities;

namespace BlueBook.DataAccess.Mappings
{
    public class FieldForceAdressMapping:EntityMapping<FieldForceAddress>
    {
        public FieldForceAdressMapping()
        {
            Property(x => x.AddressLine1).IsRequired().HasMaxLength(255);
            Property(x => x.AddressLine2).IsOptional().HasMaxLength(255);
            Property(x => x.City).IsRequired().HasMaxLength(255);
            Property(x => x.State).IsRequired().HasMaxLength(50);
            Property(x => x.Zip).IsRequired().HasMaxLength(50);

            HasRequired(x => x.FieldForce).WithRequiredDependent(y => y.Address).WillCascadeOnDelete(false);
        }
    }
}
