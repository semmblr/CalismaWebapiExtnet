using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalismaWebapiExtnet.Model
{
    public class Get_OrderSummaryByTreeDetail_Model : Db.Tables.OrderDetails
    {
        public double Price { get; set; }
        public string Unit { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string SupplierName { get; set; }

        public double Amount
        {
            get
            {

                return (Price * Quantity) ?? 0;
            }
        }

    }
}
