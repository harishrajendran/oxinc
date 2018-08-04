using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oxinc.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
    }
}