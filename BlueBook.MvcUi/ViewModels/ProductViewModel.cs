using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlueBook.MvcUi.ViewModels
{
    public class ProductViewModel
    {
        public int? Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double? Price { get; set; }
        public string BrandName { get; set; }
        [Required]
        public int BrandId { get; set; }
    }
}