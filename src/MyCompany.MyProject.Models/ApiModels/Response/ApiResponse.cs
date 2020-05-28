using MyCompany.MyProject.Models.ApiModels.Response.Error;
using System.Text.Json.Serialization;

namespace MyCompany.MyProject.Models.ApiModels.Response
{
    public class ApiResponse<TResponseData> where TResponseData : class
    {
        public TResponseData Data { get; set; }
        [JsonPropertyName("error")]
        public ApiError Error { get; set; }
    }
}
