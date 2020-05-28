using System.Text.Json.Serialization;

namespace MyCompany.MyProject.Models.ApiModels.Response.ResponseData
{
    public class DeleteResponseData
    {
        [JsonPropertyName("is_deleted")]
        public bool IsDeleted { get; set; }
    }
}
