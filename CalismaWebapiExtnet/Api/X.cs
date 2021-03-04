using CalismaWebapiExtnet.Model;
using Db.Tables;
using Microsoft.AspNetCore.Mvc;
using Odb;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Api
{
    public class X
    {
        private Orders t0 = new Orders();    //instance 
        private OrderDetails t1 = new OrderDetails();
        private Categories t3 = new Categories();
        private Shippers t4 = new Shippers();
        private Employees t5 = new Employees();
        private Products t6 = new Products();
        private Customers t7 = new Customers();
        private Suppliers t8 = new Suppliers();


        public IEnumerable<Partners> Get_Partners()
        {
            var sql1 = Odb.SQL.Select(x =>
                           x.From(t7)
                           .Fields(x["C"].AS("Type"),
                           x[t7.CustomerID].AS("ID"),
                           x[t7.CustomerName].AS("Name"),
                           t7.Address, t7.City, t7.ContactName, t7.Country, t7.PostalCode, t7.EnDis));

            var sql2 = Odb.SQL.Select(x =>
                            x.From(t8)
                            .Fields(x["S"].AS("Type"),
                                    x[t8.SupplierID].AS("ID"),
                                    x[t8.SupplierName].AS("Name"),
                                    t8.Address, t8.City, t8.ContactName, t8.Country, t8.PostalCode, t8.EnDis));

            var sqlpartners = Odb.SQL.Union(x =>
                            x.Add(sql1)
                             .Add(sql2)
                        );

            var rows = sqlpartners.ExecuteReader<Partners>(Db.X.cn);
            return rows;

        }

        //chartdata
        public List<Db.Tables.OrderReport> Get_OrderSummaryByPartners()
        {
            var sql_cust = Odb.SQL.Select(x =>
                                        x.From(t0)
                                        .LeftJoin(t7, t7.CustomerID == t0.CustomerID)
                                        .Fields(x["C"].AS("Type"),
                                        x[t7.CustomerName].AS("Name"), x[t0.OrderID].COUNT().AS("OrderCount"))
                                        .GroupBy(t7.CustomerName));

            var sql_emp = Odb.SQL.Select(x =>
                                        x.From(t0)
                                         .LeftJoin(t5, t5.EmployeeID == t0.EmployeeID)
                                        .Fields(x["E"].AS("Type"),
                                        x[t5.FirstName].AS("Name"), x[t0.OrderID].COUNT().AS("OrderCount"))
                                        .GroupBy(t5.FirstName)
                                        );

            var sql_ship = Odb.SQL.Select(x =>
                                        x.From(t0)
                                        .LeftJoin(t4, t4.ShipperID == t0.ShipperID)
                                        .Fields(x["S"].AS("Type"),
                                        x[t4.ShipperName].AS("Name"), x[t0.OrderID].COUNT().AS("OrderCount"))
                                        .GroupBy(t4.ShipperName));

            var sql_chart_data = Odb.SQL.Union(x =>
                            x.Add(sql_cust)
                             .Add(sql_emp)
                             .Add(sql_ship)).ExecuteReader<Db.Tables.OrderReport>(Db.X.cn).ToList();


            return sql_chart_data;
        }


        public IEnumerable<Get_OrderSummaryByTree_Model> Get_OrderSummaryByTree()
        {
            var sql = Odb.SQL.Select(x => x.From(t0)
                                              .Fields(t0.OrderID,
                                              t7.CustomerName, t6.ProductName, t6.Price, t6.Unit,
                                              t8.SupplierName, t3.CategoryName, t4.ShipperName, t1.OrderDetailID,
                                              t5.FirstName, t1.Quantity)
                                              .InnerJoin(t1, t1.OrderID == t0.OrderID)
                                              .InnerJoin(t7, t7.CustomerID == t0.CustomerID)
                                              .InnerJoin(t6, t6.ProductID == t1.ProductID)
                                              .InnerJoin(t4, t4.ShipperID == t0.ShipperID)
                                              .InnerJoin(t5, t5.EmployeeID == t0.EmployeeID)
                                              .InnerJoin(t8, t8.SupplierID == t6.SupplierID)
                                              .InnerJoin(t3, t3.CategoryID == t6.CategoryID));



            var result = new List<Get_OrderSummaryByTree_Model>();

            Get_OrderSummaryByTree_Model new_item = null;

            foreach (var item in sql.ExecuteReader(Db.X.cn))
            {
                var id = item.GetInt32(0);

                if (new_item?.OrderID != id)
                {
                    new_item = new Get_OrderSummaryByTree_Model()
                    {
                        OrderID = id,
                        CustomerName = item.GetString(1),
                        ShipperName = item.GetString(7),
                        FirstName = item.GetString(9),

                        Details = new List<Get_OrderSummaryByTreeDetail_Model>()
                    };
                    result.Add(new_item);
                }

                new_item.Details.Add(new Get_OrderSummaryByTreeDetail_Model()
                {
                    OrderID = id,
                    ProductName = item.GetString(2),
                    Price = item.GetDouble(3),
                    Unit = item.GetString(4),
                    CategoryName = item.GetString(5),
                    SupplierName = item.GetString(6),
                    OrderDetailID = item.GetInt32(8),
                    Quantity = item.GetInt32(10),
                });

            }
            return result;
        }



    }
}


