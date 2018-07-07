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
        public MarketHierarchyDto()
        {
            Chields = new List<MarketHierarchyDto>();
        }

        public int? Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public int? ParentId { get; set; }

        public List<MarketHierarchyDto> Chields { get; set; }

        [Required]
        public MarketHierarchyType Type { get; set; }
    }
}