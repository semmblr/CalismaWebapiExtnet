using Db.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Odb;


namespace Api.ApiSupplier
{
    public class X
    {
        private Suppliers t8 = new Suppliers();

        public void Create(string SupplierName, string ContactName, string Address,
                                 string City, string PostalCode, string Country)
        {

            var supplier_add = Odb.SQL.Insert();
            var maxid = Odb.SQL.Select(x => x.From(t8).Fields(x[t8.SupplierID].MAX()))
                                              .ExecuteReader<int>(Db.X.cn).FirstOrDefault();

            supplier_add.Add(new Suppliers()
            {
                SupplierID = maxid + 1,
                ContactName = ContactName,
                Address = Address,
                City = City,
                PostalCode = PostalCode,
                Country = Country,

            });

            var rows = supplier_add.ExecuteNonQuery(Db.X.cn);
        }


        public List<Db.Tables.Suppliers> List()
        {
            return Odb.SQL.Select(x => x.From(t8)).ExecuteReader<Db.Tables.Suppliers>(Db.X.cn).ToList();
        }

        //public string Update(Suppliers supplier)
        //{
        //    int id = (int)supplier.SupplierID;
        //    var sql = Odb.SQL.Update(x => x.From(t8).Where(t8.SupplierID == id)
        //    .SetModel(supplier)).ExecuteNonQuery(Db.X.cn);

        //    return "Okey";
        //}

        public void Update(string ContactName, string Address, string City, string PostalCode, string Country, int SupplierID)
        {
            int ID = SupplierID;
            var sql = Odb.SQL.Update(x => x.From(t8).Where(t8.SupplierID == ID)
                                        .Set(t8.ContactName, ContactName)
                                        .Set(t8.Address, Address)
                                        .Set(t8.City, City)
                                        .Set(t8.PostalCode, PostalCode)
                                        .Set(t8.Country, Country)
                                        .Set(t8.SupplierID, SupplierID));
            var rows = sql.ExecuteNonQuery(Db.X.cn);
        }


        public void Delete(int ID)
        {
            var sql = Odb.SQL.Query();
            sql.Delete(x => x.From(t8).Where(t8.SupplierID == ID)).ExecuteNonQuery(Db.X.cn);
        }

        public void EnableDisable(bool EnDis, int ID)
        {
            
            // var rows = Odb.SQL.Select(x => x.From(t3)).ExecuteReader<Db.Tables.Categories>(Db.X.cn);
            if (t8.EnDis == true)
            {
                var sql = Odb.SQL.Update(x => x.From(t8).Where(t8.SupplierID == ID).SetRaw(t8.EnDis, false)).ExecuteNonQuery(Db.X.cn);
            }
            else
            {
                var sql = Odb.SQL.Update(x => x.From(t8).Where(t8.SupplierID == ID).SetRaw(t8.EnDis, true)).ExecuteNonQuery(Db.X.cn);
            }
        }
    }
}
