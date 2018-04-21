using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlueBook.MvcUi.ViewModels
{
    public class FieldForceViewModel
    {
        public int? Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string MarketHierarchy { get; set; }
        public int? MarketHierarchyId { get; set; }
        public List<int> DistributorIds { get; set; }
        public List<string> Distributors { get; set; }
        [Required]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string ZipCode { get; set; }
    }
}