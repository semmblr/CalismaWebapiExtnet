using Db.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using Odb;

using System.Threading.Tasks;

namespace Api.ApiCategory
{
    public class X
    {
        private Categories t3 = new Categories();
        public void Create(string CategoryName, string Description)
        {
            var category_add = Odb.SQL.Insert();

            var maxid = Odb.SQL.Select(x => x.From(t3).Fields(x[t3.CategoryID].MAX()))
                                              .ExecuteReader<int>(Db.X.cn).FirstOrDefault();
            category_add.Add(new Categories()
            {
                CategoryID = maxid + 1,
                CategoryName = CategoryName,
                Description = Description,
            });
            var rows = category_add.ExecuteNonQuery(Db.X.cn);
        }

        public IEnumerable<Db.Tables.Categories> List()
        {
            var sql = Odb.SQL.Select(x => x.From(t3));
            var rows = sql.ExecuteReader<Db.Tables.Categories>(Db.X.cn);
            return rows;
        }

        public void Update(string CategoryName, string Description, int CategoryID)
        {
            int id = CategoryID;
            var sql = Odb.SQL.Update(x => x.From(t3).Where(t3.CategoryID == id)
                                        .Set(t3.CategoryName, CategoryName)
                                        .Set(t3.Description, Description));
            var rows = sql.ExecuteNonQuery(Db.X.cn);
        }

        public void Delete(int id)
        {
            var sql = Odb.SQL.Query();

            sql.Delete(x => x.From(t3).Where(t3.CategoryID == id)).ExecuteNonQuery(Db.X.cn);
        }

        public void EnableDisable(bool EnDis, int CategoryID)
        {
            int id = CategoryID;
            //var rows = Odb.SQL.Select(x => x.From(t3)).ExecuteReader<Db.Tables.Categories>(Db.X.cn);
            if (EnDis == true)
            {
               
                var sql = Odb.SQL.Update(x =>x.From(t3).Where(t3.CategoryID == id).SetRaw(t3.EnDis, false)).ExecuteNonQuery(Db.X.cn);
            }
            else if(EnDis==false)
            {
                var sql = Odb.SQL.Update(x =>x.From(t3).Where(t3.CategoryID == id).SetRaw(t3.EnDis, true)).ExecuteNonQuery(Db.X.cn);
            }
        }
    }
}
