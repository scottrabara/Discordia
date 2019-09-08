using System;

using System.Globalization;
using Discordia.Abstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Discordia.Data.EventData
{

    public partial class MessageCreateEventData : IEventData
    {
        [JsonProperty("type")]
        public long Type { get; set; }

        [JsonProperty("tts")]
        public bool Tts { get; set; }

        [JsonProperty("timestamp")]
        public DateTimeOffset Timestamp { get; set; }

        [JsonProperty("pinned")]
        public bool Pinned { get; set; }

        [JsonProperty("nonce")]
        public string Nonce { get; set; }

        [JsonProperty("mentions")]
        public object[] Mentions { get; set; }

        [JsonProperty("mention_roles")]
        public object[] MentionRoles { get; set; }

        [JsonProperty("mention_everyone")]
        public bool MentionEveryone { get; set; }

        [JsonProperty("member")]
        public Member Member { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("embeds")]
        public object[] Embeds { get; set; }

        [JsonProperty("edited_timestamp")]
        public object EditedTimestamp { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("channel_id")]
        public string ChannelId { get; set; }

        [JsonProperty("author")]
        public Author Author { get; set; }

        [JsonProperty("attachments")]
        public object[] Attachments { get; set; }

        [JsonProperty("guild_id")]
        public string GuildId { get; set; }
    }

    public partial class Author
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("discriminator")]
        public long Discriminator { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }
    }

    public partial class Member
    {
        [JsonProperty("roles")]
        public object[] Roles { get; set; }

        [JsonProperty("nick")]
        public string Nick { get; set; }

        [JsonProperty("mute")]
        public bool Mute { get; set; }

        [JsonProperty("joined_at")]
        public DateTimeOffset JoinedAt { get; set; }

        [JsonProperty("hoisted_role")]
        public object HoistedRole { get; set; }

        [JsonProperty("deaf")]
        public bool Deaf { get; set; }
    }
}
