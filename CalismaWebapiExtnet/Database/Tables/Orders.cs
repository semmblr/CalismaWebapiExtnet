using Odb.Table;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Db.Tables
{
    public class Orders : Base
    {

        [Column()] public int OrderID { get; set; } //strinler class dır ve class lar enable tanımlanmaz
        [Column()] public int? CustomerID { get; set; }
        [Column()] public int? EmployeeID { get; set; }
        [Column()] public DateTime? OrderDate { get; set; }
        [Column()] public int? ShipperID { get; set; }
        [Column()] public bool EnDis { get; set; }


    }
}