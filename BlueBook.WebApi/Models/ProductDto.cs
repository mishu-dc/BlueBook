using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlueBook.WebApi.Models
{
    public class ProductDto
    {
        public int? id { get; set; }
        [Required]
        public string code { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public int brandId { get; set; }
        public string brandName { get; set; }
        [Required]
        public double price { get; set; }
    }
}