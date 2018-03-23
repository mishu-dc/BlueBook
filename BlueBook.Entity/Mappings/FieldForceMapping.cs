using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueBook.DataAccess.Entities;

namespace BlueBook.DataAccess.Mappings
{
    public class FieldForceMapping:EntityMapping<FieldForce>
    {
        public FieldForceMapping()
        {
            Property(x => x.Code).IsRequired().HasMaxLength(25);
            Property(x => x.Name).IsRequired().HasMaxLength(255);
            Property(x => x.Phone).IsOptional().HasMaxLength(255);
            Property(x => x.Email).IsOptional().HasMaxLength(255);

            HasOptional(x => x.MarketHierarchy).WithMany(y => y.FieldForces).HasForeignKey(y => y.MarketHierarchyId);

            HasRequired(x => x.Address).WithRequiredPrincipal(y => y.FieldForce).WillCascadeOnDelete(false);
            
            HasMany(x => x.Distributors).WithMany(y => y.FieldForces)
                .Map(m => m.ToTable("FieldForceDistributors")
                        .MapLeftKey("FieldForceId")
                        .MapRightKey("DistributorId"));

        }
    }
}
