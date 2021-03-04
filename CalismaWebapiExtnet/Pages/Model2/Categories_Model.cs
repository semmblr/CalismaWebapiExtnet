using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CalismaWebapiExtnet.Pages.Model2
{
    public class Categories_Model
    {
        /* [JsonProperty("cat_cId")]*/

        /* [JsonProperty("cat_nam")]*/
        public string CategoryName { get; set; }

        /* [JsonProperty("cat_dsc")]*/
        public string Description { get; set; }
        public int CategoryID { get; set; }

        /* [JsonProperty("cat_end")]*/
        public bool EnDis { get; set; }

    }


}
