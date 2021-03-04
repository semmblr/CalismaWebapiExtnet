using Odb.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Db.Tables
{
    public class Products : Base
    {
        [Column()] public int ProductID { get; set; }

        [Column()] public string ProductName { get; set; }

        [Column()] public int SupplierID { get; set; }

        [Column()] public int CategoryID { get; set; }

        [Column()] public string Unit { get; set; }

        [Column()] public double Price { get; set; }


    }
}
