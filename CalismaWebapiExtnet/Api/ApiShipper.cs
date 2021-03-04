using Db.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Odb;


namespace Api.ApiShipper
{
    public class X
    {
        private Shippers t4 = new Shippers();

        public void Create(string ShipperName, string Phone)
        {
            var shipper_add = Odb.SQL.Insert();
            var maxid = Odb.SQL.Select(x => x.From(t4).Fields(x[t4.ShipperID].MAX()))
                                               .ExecuteReader<int>(Db.X.cn).FirstOrDefault();

            shipper_add.Add(new Shippers()
            {
                ShipperID = maxid + 1,
                ShipperName = ShipperName,
                Phone = Phone,
            });
            var rows = shipper_add.ExecuteNonQuery(Db.X.cn);
        }

        public List<Db.Tables.Shippers> List()
        {
            return Odb.SQL.Select(x => x.From(t4)).ExecuteReader<Db.Tables.Shippers>(Db.X.cn).ToList();
        }
      
        public void Update(string ShipperName, string Phone, int ShipperID )
        {
            int id = ShipperID;
            var sql = Odb.SQL.Update(x => x.From(t4).Where(t4.ShipperID == id)
                                        .Set(t4.ShipperName, ShipperName)
                                        .Set(t4.Phone, Phone));
            var rows = sql.ExecuteNonQuery(Db.X.cn);
        }

        public void Delete(int id)
        {
            var sql = Odb.SQL.Query();
            sql.Delete(x => x.From(t4).Where(t4.ShipperID == id)).ExecuteNonQuery(Db.X.cn);
        }

        public void EnableDisable(bool EnDis,int ShipperID)
        {
            int id = ShipperID;
            // var rows = Odb.SQL.Select(x => x.From(t4)).ExecuteReader<Db.Tables.Categories>(Db.X.cn);
            if (EnDis == false)
            {
                var sql = Odb.SQL.Update(x => x.From(t4).Where(t4.ShipperID == id).SetRaw(t4.EnDis, true)).ExecuteNonQuery(Db.X.cn);
            }
            else if(EnDis == true)
            {
                var sql = Odb.SQL.Update(x => x.From(t4).Where(t4.ShipperID == id).SetRaw(t4.EnDis, false)).ExecuteNonQuery(Db.X.cn);
            }
        }

    }
}
