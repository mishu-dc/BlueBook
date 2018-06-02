using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlueBook.WebApi.Models
{
    public class BrandDto
    {
        public int? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}