using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueBook.DataAccess.Entities;

namespace BlueBook.DataAccess.Mappings
{
    public class MarketHierarchyMapping:EntityMapping<MarketHierarchy>
    {
        public MarketHierarchyMapping()
        {
            Property(x => x.Code).IsRequired().HasMaxLength(25);
            Property(x => x.Name).IsRequired().HasMaxLength(255);
            Property(x => x.Type).IsRequired();
            Property(x => x.ParentId).IsOptional();

            //This is optional. Fieldforce has a mapping for Markethierarchy
            //HasMany(x => x.FieldForces).WithOptional(y => y.MarketHierarchy).HasForeignKey(x => x.MarketHierarchyId).WillCascadeOnDelete(false);
        }
    }
}
