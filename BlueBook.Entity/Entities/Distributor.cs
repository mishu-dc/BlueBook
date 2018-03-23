using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBook.DataAccess.Entities
{
    public class Distributor: EntityBase
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<FieldForce> FieldForces { get; set; }
    }
}
