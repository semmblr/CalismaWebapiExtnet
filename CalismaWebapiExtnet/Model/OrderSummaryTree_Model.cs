using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalismaWebapiExtnet.Pages.Model2
{
    public class OrderDetails
    {

        public string OrderDetailID { get; set; }

        public string ProductName { get; set; }
        public double Price { get; set; }
        public string Unit { get; set; }

        public double Quantity { get; set; }

        public double Amount;
        public string CategoryName { get; set; }
        public string SupplierName { get; set; }


    }


    public class Order
    {
        public int OrderID { get; set; }
        public string CustomerName { get; set; }
        public string FirstName { get; set; }
        public string ShipperName { get; set; }
        public List<OrderDetails> Details { get; set; }

    }

}
