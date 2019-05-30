using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JewelleryShop.Models
{
    public class Cart
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public int productPrice { get; set; }
        public string productImage { get; set; }
        public int qty { get; set; }
    }
}