using Odb.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Db.Tables
{
    public class OrderReport
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string OrderCount { get; set; }
    }
}
