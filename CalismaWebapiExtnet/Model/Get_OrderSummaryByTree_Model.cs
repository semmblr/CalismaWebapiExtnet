using Odb.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalismaWebapiExtnet.Model
{
    public class Get_OrderSummaryByTree_Model : Db.Tables.Orders
    {
        public List<Get_OrderSummaryByTreeDetail_Model> Details { get; set; }
        public string CustomerName { get; set; }
        public string ShipperName { get; set; }
        public string FirstName { get; set; }
    }
   

}
