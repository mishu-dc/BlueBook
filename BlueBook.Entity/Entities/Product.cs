using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBook.DataAccess.Entities
{
    public class Product: EntityBase
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public virtual Brand Brand { get; set; }
        public int BrandId { get; set; }
        public double Price { get; set; }
    }
}
