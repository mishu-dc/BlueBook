using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBook.DataAccess.Entities
{
    public enum MarketHierarchyType
    {
        Nation = 1,
        Region = 2,
        Territory = 3,
        Route = 4
    }

    public class MarketHierarchy : EntityBase
    {   
        public String Code { get; set; }
        public String Name { get; set; }
        public MarketHierarchy Parent { get; set; }
        public int? ParentId { get; set; }
        public MarketHierarchyType Type { get; set; }
        public List<FieldForce> FieldForces { get; set; }
        public List<MarketHierarchy> MarketHierarchies { get; set; }
    }
}
