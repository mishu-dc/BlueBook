using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueBook.DataAccess.Entities;

namespace BlueBook.DataAccess.Mappings
{
    public class BrandMapping:EntityMapping<Brand>
    {
        public BrandMapping()
        {
            Property(x => x.Code).IsRequired().HasMaxLength(25);
            Property(x => x.Name).IsRequired().HasMaxLength(255);
            HasMany(x => x.Products)
                .WithRequired(y => y.Brand)
                .HasForeignKey(y=>y.BrandId)
                .WillCascadeOnDelete(false);
        }
    }
}
