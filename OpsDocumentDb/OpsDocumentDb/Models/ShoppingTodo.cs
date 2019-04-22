using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpsDocumentDb
{
    public class ShoppingTodo
    {
    
            [Newtonsoft.Json.JsonProperty(PropertyName = "id")]
            public string Id { get; set; }
            [Newtonsoft.Json.JsonProperty(PropertyName = "item")]
            public string Item { get; set; }
            [Newtonsoft.Json.JsonProperty(PropertyName = "done")]
            public bool Done { get; set; }
            [Newtonsoft.Json.JsonProperty(PropertyName = "partitionkey")]
            public int PartitionKey { get; set; }
        
    }
}
