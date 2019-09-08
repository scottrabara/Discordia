using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discordia.Data.Rest
{
    public partial class CreateMessageRest
    {
        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("tts")]
        public bool Tts { get; set; }
    }

    public partial class Embed
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
