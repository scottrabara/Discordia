using Discordia.Abstractions;
using Discordia.Data.PartialData;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discordia.Data.EventData
{
    public class IdentifyEventData
    {
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("properties")]
        public IdentifyProperties Properties { get; set; }
    }
}
