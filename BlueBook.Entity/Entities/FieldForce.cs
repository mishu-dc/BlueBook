using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBook.DataAccess.Entities
{
    public class FieldForce: EntityBase
    {
        public FieldForce()
        {
            Distributors = new List<Distributor>();
        }

        public string Code { get; set; }
        public string Name { get; set; }
        public virtual List<Distributor> Distributors { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public virtual MarketHierarchy MarketHierarchy { get; set; }
        public int? MarketHierarchyId { get; set; }
        public virtual FieldForceAddress Address { get; set; }
    }
}
