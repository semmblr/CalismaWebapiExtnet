using Db.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Odb;


namespace Api.ApiCustomer
{
    public class X
    {
        private Customers t7 = new Customers();

        public void Create(string CustomerName, string ContactName, string Address,
                                 string City, string PostalCode, string Country)
        {

            var customer_add = Odb.SQL.Insert();
            var maxid = Odb.SQL.Select(x => x.From(t7).Fields(x[t7.CustomerID].MAX()))
                                              .ExecuteReader<int>(Db.X.cn).FirstOrDefault();
            customer_add.Add(new Customers()
            {
                CustomerID = maxid + 1,
                ContactName = ContactName,
                Address = Address,
                City = City,
                PostalCode = PostalCode,
                Country = Country,

            });

            var rows = customer_add.ExecuteNonQuery(Db.X.cn);
        }
        public List<Db.Tables.Customers> List()
        {
            return Odb.SQL.Select(x => x.From(t7)).ExecuteReader<Db.Tables.Customers>(Db.X.cn).ToList();

        }

        //public string Update( Customers customer)
        //{
        //    int id = (int)customer.CustomerID;
        //    var sql = Odb.SQL.Update(x => x.From(t7).Where(t7.CustomerID == id)
        //    .SetModel(customer)).ExecuteNonQuery(Db.X.cn);

        //    return "Okey";
        //}

        public void Update(string ContactName, string Address, string City, string PostalCode, string Country,int CustomerID)
        {
            int ID = CustomerID;
            var sql = Odb.SQL.Update(x => x.From(t7).Where(t7.CustomerID == ID)
                                        .Set(t7.ContactName, ContactName)
                                        .Set(t7.Address, Address)
                                        .Set(t7.City, City)
                                        .Set(t7.PostalCode, PostalCode)
                                        .Set(t7.Country, Country)
                                        .Set(t7.CustomerID, CustomerID));
            var rows = sql.ExecuteNonQuery(Db.X.cn);

        }

        public void Delete(int ID)
        {
            var sql = Odb.SQL.Query();
            sql.Delete(x => x.From(t7).Where(t7.CustomerID == ID)).ExecuteNonQuery(Db.X.cn);
        }

        public void EnableDisable(bool Endis, int ID)
        {
            // var rows = Odb.SQL.Select(x => x.From(t3)).ExecuteReader<Db.Tables.Categories>(Db.X.cn);
            if (t7.EnDis == true)
            {
                var sql = Odb.SQL.Update(x => x.From(t7).Where(t7.CustomerID == ID).SetRaw(t7.EnDis, false)).ExecuteNonQuery(Db.X.cn);
            }
            else
            {
                var sql = Odb.SQL.Update(x => x.From(t7).Where(t7.CustomerID == ID).SetRaw(t7.EnDis, true)).ExecuteNonQuery(Db.X.cn);
            }
        }
    }
}
