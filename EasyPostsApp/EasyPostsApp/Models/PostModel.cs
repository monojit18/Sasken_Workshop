using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace EasyPostApp.Models
{
    public class PostModel
    {

        
        [JsonProperty("Id")]
        public ObjectId _id { get; set; }


        [JsonProperty("Post")]
        public string message { get; set; }

        [JsonProperty("DateTime")]
        public string when { get; set; }


    }
}