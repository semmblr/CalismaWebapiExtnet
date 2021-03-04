using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalismaWebapiExtnet.Pages.Model2
{
    public class Partners_Model
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public int SupplierID { get; set; }
       public string Name { get; set; }
        public string ContactName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Type { get; set; }
        public bool EnDis { get; set; }


    }


}
