using BlueBook.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlueBook.WebApi.Models
{
    public class FieldForceDto
    {
        public int? Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual List<DistributorDto> Distributors { get; set; }
        public virtual List<int> DistributorIds { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public virtual MarketHierarchyDto MarketHierarchy { get; set; }
        public int? MarketHierarchyId { get; set; }
        [Required]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Zip { get; set; }
    }
}