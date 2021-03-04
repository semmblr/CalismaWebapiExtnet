using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Model.Model_Categories
{
    public class Model_Categories
    {
        [JsonPropertyName("cat_cId")] public int? CategoryID { get; set; }
        [JsonPropertyName("cat_nam")] public string CategoryName { get; set; }
        [JsonPropertyName("cat_dsc")] public string Description { get; set; }
        [JsonPropertyName("cat_end")] public bool EnDis { get; set; }

    }
}
