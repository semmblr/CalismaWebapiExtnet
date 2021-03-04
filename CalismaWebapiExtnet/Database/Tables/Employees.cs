using Odb.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Db.Tables
{
    public class Employees : Base
    {
        [Column()] public int? EmployeeID { get; set; }

        [Column()] public string LastName { get; set; }
        [Column()] public string FirstName { get; set; }
        [Column()] public DateTime Birthdate { get; set; }

        [Column()] public string Photo { get; set; }
        [Column()] public string Notes { get; set; }
        [Column()] public bool EnDis { get; set; }


    }
}
