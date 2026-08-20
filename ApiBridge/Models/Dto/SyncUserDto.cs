
using System.Text.Json.Serialization;

namespace ApiBridge.Models.Dto
{
    public class SyncUserDto

    {
        [JsonPropertyName("external_id")]
        public string? External_id { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("email")]
        public string? Email { get; set; }
        [JsonPropertyName("payment_status")]
        public string? Payment_status { get; set; }
        [JsonPropertyName("expires_in_minutes")]
        public int expires_in_minutes { get; set; }
        [JsonPropertyName("type")]
        public string type { get; set; }
        [JsonPropertyName("code")]
        public string code { get; set; }
        public SyncUserDto()
        {
        }
    }
}

