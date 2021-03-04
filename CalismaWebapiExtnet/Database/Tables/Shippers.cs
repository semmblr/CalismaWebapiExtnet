using Odb.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Db.Tables
{
    public class Shippers : Base
    {

        //[JsonPropertyName("s_id")] [Column()] public int? ShipperID { get; set; }
        //[JsonPropertyName("s_name")] [Column()] public string ShipperName { get; set; }
        //[JsonPropertyName("s_phone")] [Column()] public string Phone { get; set; }


        [Column()] public int ShipperID { get; set; }
        [Column()] public string ShipperName { get; set; }
        [Column()] public string Phone { get; set; }
        [Column()] public bool EnDis { get; set; }


    }
}
