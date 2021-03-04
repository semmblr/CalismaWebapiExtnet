﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model.Model_Customers
{
    public class Model_Customers
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string ContactName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public bool EnDis { get; set; }

    }
}
