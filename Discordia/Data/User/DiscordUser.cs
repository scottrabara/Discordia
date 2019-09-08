using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discordia.Data.User
{
    public partial class DiscordUser
    {
        [JsonProperty("verified")]
        public bool Verified { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("mfa_enabled")]
        public bool MfaEnabled { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("email")]
        public object Email { get; set; }

        [JsonProperty("discriminator")]
        public long Discriminator { get; set; }

        [JsonProperty("bot")]
        public bool Bot { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        [JsonIgnore]
        public string Token { get; set; }
    }
}
