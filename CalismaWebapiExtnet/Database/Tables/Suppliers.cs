using Odb.Table;
using System.Text.Json.Serialization;

namespace Db.Tables
{
    public class Suppliers : Base
    {

        [Column()] public int SupplierID { get; set; }
        [Column()] public string SupplierName { get; set; }
        [Column()] public string ContactName { get; set; }
        [Column()] public string Address { get; set; }
        [Column()] public string City { get; set; }
        [Column()] public string PostalCode { get; set; }
        [Column()] public string Country { get; set; }
        [Column()] public bool EnDis { get; set; }

    }

}

