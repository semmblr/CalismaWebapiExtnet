using Db.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using Odb;

using System.Threading.Tasks;

namespace Api.ApiOrder
{
    public class X
    {
        private Orders t0 = new Orders();    //instance 
        private OrderDetails t1 = new OrderDetails();


        public void Create(int OrderID, int CustomerID, int EmployeeID,
                                 DateTime OrderDate, int ShipperID)
        {

            var order_add = Odb.SQL.Insert();
            var maxid = Odb.SQL.Select(x => x.From(t0).Fields(x[t0.OrderID].MAX()))
                                              .ExecuteReader<int>(Db.X.cn).FirstOrDefault();
            order_add.Add(new Orders()
            {
                OrderID = maxid + 1,
                CustomerID = CustomerID,
                EmployeeID = EmployeeID,
                OrderDate = OrderDate,
                ShipperID = ShipperID,
            });

            var rows = order_add.ExecuteNonQuery(Db.X.cn);
        }



        public IEnumerable<Orders> List(int? EmployeeID, int? CustomerID, DateTime? OrderDate, DateTime? OrderDate2)
        {

            var sql = Odb.SQL.Select(x => x.From(t0));

            if (CustomerID.HasValue)    //HasValue bool, struct yapısındadır. class olan stringlerde kullanılmaz 
                sql.Add(x => x.Where(t0.CustomerID == CustomerID));  //linq yazım
            if (EmployeeID.HasValue)
                sql.Add(x => x.Where(t0.EmployeeID == EmployeeID));
            if (OrderDate.HasValue)
                sql.Add(x => x.Where(t0.OrderDate >= OrderDate));
            if (OrderDate2.HasValue)
                sql.Add(x => x.Where(t0.OrderDate <= OrderDate2));

            return sql.ExecuteReader<Orders>(Db.X.cn).ToList();

        }

        public string Update(int? OrderID, DateTime? OrderDate1,  DateTime? OrderDate2)

        {

            var sub_sql = Odb.SQL.Select(x => x.From(t0).Where(t0.OrderDate >= OrderDate1 &
                                                                t0.OrderDate <= OrderDate2).Fields(x[t0.OrderID]));
            //fields, veritabanı alanları. dıstınc aynı olanlardan bir tane alır

            var sqlupdate = Odb.SQL.Update(x => x.From(t1).Where(x[t1.OrderID].IN(sub_sql)).SetRaw(t1.Quantity, t1.Quantity * 2));
            // IN Yukarıda seçtiğimiz(sub_sql) select i update edeceğiz.
            //SetRaw sql içinden işlem yapılıyor. set .net üzerinden işlem yapıyor. 

            var affectedRows = sqlupdate.ExecuteNonQuery(Db.X.cn);
            //ExecuteNonQuery ile update delete insert leri yapmamıza 

            //var s = Odb.SQL.Select(x => x.From(t1).Fields(x[t1.OrderID].DISTINCT()));
            //var rows = s.ExecuteReader<Db.Tables.OrderDetails>(Db.X.cn).ToArray();


            return "Okey";


        }

        public string Delete(int OrderID1)
        {
            var sql = Odb.SQL.Query();
            sql.Delete(x => x.From(t1).Where(t1.OrderID == OrderID1));
            sql.Delete(x => x.From(t0).Where(t0.OrderID == OrderID1));
            var affectedRows = sql.ExecuteNonQuery(Db.X.cn);
            return "Okey";
        }

        public void EnableDisable()
        {
            // var rows = Odb.SQL.Select(x => x.From(t3)).ExecuteReader<Db.Tables.Categories>(Db.X.cn);
            if (t0.EnDis == true)
            {
                var sql = Odb.SQL.Update(x => x.From(t0).SetRaw(t0.EnDis, false)).ExecuteNonQuery(Db.X.cn);
            }
            else
            {
                var sql = Odb.SQL.Update(x => x.From(t0).SetRaw(t0.EnDis, true)).ExecuteNonQuery(Db.X.cn);
            }
        }
    }
}
