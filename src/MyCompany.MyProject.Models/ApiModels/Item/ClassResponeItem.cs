using System.Text.Json.Serialization;

namespace MyCompany.MyProject.Models.ApiModels.Item
{
    public class ClassResponseItem
    {
        [JsonPropertyName("id")]
        public int ClassId { get; set; }
        [JsonPropertyName("name")]
        public string ClassName { get; set; }
    }
}
