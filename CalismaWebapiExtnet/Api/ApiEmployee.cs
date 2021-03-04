using Db.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Odb;


namespace Api.ApiEmployee
{
    public class X
    {
        private Employees t5 = new Employees();


        public void Create(string FirstName, string LastName)
        {
            var employee_add = Odb.SQL.Insert();
            var maxid = Odb.SQL.Select(x => x.From(t5).Fields(x[t5.EmployeeID].MAX()))
                                               .ExecuteReader<int>(Db.X.cn).FirstOrDefault();

            employee_add.Add(new Employees()
            {
                EmployeeID = maxid + 1,
                FirstName = FirstName,
                LastName = LastName,
            });
            var rows = employee_add.ExecuteNonQuery(Db.X.cn);
        }


        public List<Db.Tables.Employees> List()
        {
            var sql = Odb.SQL.Select(x => x.From(t5));
            var rows = sql.ExecuteReader<Db.Tables.Employees>(Db.X.cn).ToList();
            return rows;
        }

        public string Update(Employees employee)
        {
            int id = (int)employee.EmployeeID;
            var sql = Odb.SQL.Update(x => x.From(t5).Where(t5.EmployeeID == id)
            .SetModel(employee)).ExecuteNonQuery(Db.X.cn);

            return "Okey";
        }

        public string Delete(int id)
        {
            var sql = Odb.SQL.Query();
            sql.Delete(x => x.From(t5).Where(t5.EmployeeID == id)).ExecuteNonQuery(Db.X.cn);

            return "Okey";
        }

        public void EnableDisable()
        {
            // var rows = Odb.SQL.Select(x => x.From(t3)).ExecuteReader<Db.Tables.Categories>(Db.X.cn);
            if (t5.EnDis == true)
            {
                var sql = Odb.SQL.Update(x => x.From(t5).SetRaw(t5.EnDis, false)).ExecuteNonQuery(Db.X.cn);
            }
            else
            {
                var sql = Odb.SQL.Update(x => x.From(t5).SetRaw(t5.EnDis, true)).ExecuteNonQuery(Db.X.cn);
            }
        }
    }
}