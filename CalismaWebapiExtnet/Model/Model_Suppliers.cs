using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model.Model_Suppliers
{
    public class Model_Suppliers
    {
         public int SupplierID { get; set; }
         public string SupplierName { get; set; }
         public string ContactName { get; set; }
         public string Address { get; set; }
         public string City { get; set; }
         public string PostalCode { get; set; }
         public string Country { get; set; }
        public bool EnDis { get; set; }

    }
}
