using Odb.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Db.Tables
{
    public class Partners
    {
        public int ID { get; set; }

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