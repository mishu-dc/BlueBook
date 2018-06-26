using BlueBook.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlueBook.WebApi.Models
{
    public class MarketHierarchyDto
    {
        public int? id { get; set; }
        [Required]
        public string code { get; set; }
        [Required]
        public string name { get; set; }
        public int? parentId { get; set; }

        [Required]
        public MarketHierarchyType type { get; set; }
    }
}