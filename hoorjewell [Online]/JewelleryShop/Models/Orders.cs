using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JewelleryShop.Models
{
    public class Orders
    {
        public int orderId { get; set; }
        public string userName { get; set; }
        public string productName { get; set; }
        public int Qty { get; set; }
        public int price { get; set; }
        public Nullable<System.DateTime> placementDate { get; set; }
        public Nullable<System.DateTime> deliveryDate { get; set; }
        public string isDelievered { get; set; }
        public string userContact { get; set; }
        public string userAddress { get; set; }
    }
}