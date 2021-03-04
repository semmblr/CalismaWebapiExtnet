using Odb.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Db.Tables
{
    public class Customers : Base
    {
        [Column()] public int CustomerID { get; set; }
        [Column()] public string CustomerName { get; set; }
        [Column()] public string ContactName { get; set; }
        [Column()] public string Address { get; set; }
        [Column()] public string City { get; set; }
        [Column()] public string PostalCode { get; set; }
        [Column()] public string Country { get; set; }
        [Column()] public bool EnDis { get; set; }

    }
}

