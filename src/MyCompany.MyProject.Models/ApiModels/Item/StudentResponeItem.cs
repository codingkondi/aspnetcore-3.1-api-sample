using System.Text.Json.Serialization;

namespace MyCompany.MyProject.Models.ApiModels.Item
{
    public class StudentResponseItem
    {
        [JsonPropertyName("id")]
        public int StudentId { get; set; }
        [JsonPropertyName("name")]
        public string StudentName { get; set; }
    }
}
