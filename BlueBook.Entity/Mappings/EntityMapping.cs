using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueBook.DataAccess.Entities;

namespace BlueBook.DataAccess.Mappings
{
    public class EntityMapping<TEntity> : EntityTypeConfiguration<TEntity> where TEntity : EntityBase
    {
        public EntityMapping()
        {
            HasKey(x => x.Id);

            Property(x => x.CreatedBy).IsRequired();
            Property(x => x.CreatedDate).IsRequired();
            Property(x => x.UpdatedBy).IsOptional();
            Property(x => x.UpdatedDate).IsOptional();
        }
    }
}
