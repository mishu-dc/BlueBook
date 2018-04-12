using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlueBook.MvcUi.ViewModels
{
    public class MarketHierarchyViewModel
    {
        public int? Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public string Type { get; set; }
        public List<MarketHierarchyViewModel> Children { get; set; }
    }

    public class MarketHierarchyTreeViewModel
    {
        public int id { get; set; }
        public string text { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public int parentId { get; set; }
        public string flagUrl { get; set; }
        public List<MarketHierarchyTreeViewModel> children { get; set; }
    }
}