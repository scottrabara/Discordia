using Newtonsoft.Json;

namespace Discordia.Data.PartialData
{
    public class IdentifyProperties
    {
        [JsonProperty("$os")]
        public string OperatingSystem { get; set; }
        [JsonProperty("$browser")]
        public string Browser { get; set; }
        [JsonProperty("$device")]
        public string Device { get; set; }
    }
}