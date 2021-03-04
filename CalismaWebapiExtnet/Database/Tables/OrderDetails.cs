using Odb.Table;
using System;
using System.Text.Json.Serialization;

namespace Db.Tables
{
    public class OrderDetails : Base
    {

        [Column()] public int OrderDetailID { get; set; }

        [Column()] public int? OrderID { get; set; }
        [Column()] public int? ProductID { get; set; }
        [Column()] public int? Quantity { get; set; }



    }
}
