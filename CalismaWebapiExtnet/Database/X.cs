


namespace Db
{
    public class X
    {
        public static Odb.Connection cn = default;


        static X()
        {

            var str = $"MultipleActiveResultSets=true;Pooling=false;data source=localhost; Database=w3schools; user id=sa; pwd=123456";
            cn = Odb.Connection.Create<Odb.Providers.MsSql>(str);


        }
    }
}