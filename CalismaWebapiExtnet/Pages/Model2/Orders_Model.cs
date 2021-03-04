using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalismaWebapiExtnet.Pages.Model2
{
    public class Orders_Model
    {
        public string OrderID { get; set; }
        public int CustomerID { get; set; }
        public int EmployeeID { get; set; }

        //public string OrderDate { get; set; }
        public int ShipperID { get; set; }
    }
}
