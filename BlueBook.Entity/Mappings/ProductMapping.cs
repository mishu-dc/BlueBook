using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueBook.DataAccess.Entities;

namespace BlueBook.DataAccess.Mappings
{
    public class ProductMapping:EntityMapping<Product>
    {
        public ProductMapping()
        {
            Property(x => x.Code).IsRequired().HasMaxLength(25);
            Property(x => x.Name).IsRequired().HasMaxLength(255);
            HasRequired(x => x.Brand)
                .WithMany(y => y.Products)
                .HasForeignKey(y => y.BrandId)
                .WillCascadeOnDelete(false);
        }
    }
}
