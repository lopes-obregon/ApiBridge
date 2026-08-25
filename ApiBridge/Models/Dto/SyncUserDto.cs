
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
        public int ExpiresInMinutes { get; set; }
        [JsonPropertyName("type")]
        public string? Type { get; set; }
        [JsonPropertyName("code")]
        public string? Code { get; set; }
        [JsonPropertyName("create_date")]
        public string? CreateDate { get; set; }
        [JsonPropertyName("days_remaining")]
        public int DaysRemaing { get; set; }
        [JsonPropertyName("status")]
        public string? Status { get; set; }


        public SyncUserDto()
        {
        }
    }
}

