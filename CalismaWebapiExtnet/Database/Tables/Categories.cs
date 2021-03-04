


using Odb.Table;
using System.Text.Json.Serialization;

namespace Db.Tables
{
    public class Categories : Base
    {
       /* [JsonPropertyName("cat_cId")] */[Column()] public int CategoryID { get; set; }
       /* [JsonPropertyName("cat_nam")]*/ [Column()] public string CategoryName { get; set; }
       /* [JsonPropertyName("cat_dsc")]*/ [Column()] public string Description { get; set; }
       /* [JsonPropertyName("cat_end")]*/ [Column()] public bool EnDis { get; set; }
    }
}
