using Discordia.Abstractions;
using Discordia.Data.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Discordia.Data
{
    public class GatewayPayload
    {
        [JsonProperty("op")]
        public DiscordOpcodeEnum? Opcode { get; set; }
        [JsonProperty("d")]
        public JToken EventData { get; set; }
        [JsonProperty("s")]
        public int? Sequence { get; set; }
        [JsonProperty("t")]
        public string EventName { get; set; }
    }
}