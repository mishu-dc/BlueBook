using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBook.DataAccess.Entities
{
    public class Brand: EntityBase
    {
        public Brand()
        {
            Products = new List<Product>();
        }

        public string Code { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}
