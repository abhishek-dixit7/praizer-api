using Newtonsoft.Json;

namespace praizer_api.Objects
{
    public class GoogleUserInfo
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        // Add additional properties as needed
    }
}
