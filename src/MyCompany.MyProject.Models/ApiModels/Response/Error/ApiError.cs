using System.Text.Json.Serialization;

namespace MyCompany.MyProject.Models.ApiModels.Response.Error
{
    public class ApiError
    {
        [JsonPropertyName("error_code")]
        public int ErrorCode { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
