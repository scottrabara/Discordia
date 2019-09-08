using Discordia.Abstractions;
using Discordia.Data.User;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Discordia.Data.EventData
{
    public partial class ReadyEventData : IEventData
    {
        [JsonProperty("v")]
        public long Version { get; set; }

        [JsonProperty("user_settings")]
        public UserSettings UserSettings { get; set; }

        [JsonProperty("user")]
        public DiscordUser User { get; set; }

        [JsonProperty("session_id")]
        public string SessionId { get; set; }

        [JsonProperty("relationships")]
        public object[] Relationships { get; set; }

        [JsonProperty("private_channels")]
        public object[] PrivateChannels { get; set; }

        [JsonProperty("presences")]
        public object[] Presences { get; set; }

        [JsonProperty("guilds")]
        public Guild[] Guilds { get; set; }
    }

    public partial class Guild
    {
        [JsonProperty("unavailable")]
        public bool Unavailable { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public partial class UserSettings
    {
    }
}
