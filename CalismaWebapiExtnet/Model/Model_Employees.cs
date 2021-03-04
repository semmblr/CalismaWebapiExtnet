using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model.Model_Employees
{
    public class Model_Employees
    {
        public int? EmployeeID { get; set; }

        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime Birthdate { get; set; }

        public string Photo { get; set; }
        public string Notes { get; set; }

    }
}
