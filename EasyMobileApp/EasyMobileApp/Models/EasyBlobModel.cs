using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace EasyMobileApp.Models
{

    public class EasyBlobModel
    {

        [JsonProperty("name")]
        public string BlobName { get; set; }

        [JsonProperty("contents")]
        public string BlobContents { get; set; }

        public string BlobExceptionMessage { get; set; }


    }
}