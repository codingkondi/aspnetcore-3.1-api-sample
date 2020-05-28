using MyCompany.MyProject.Models.ApiModels.Item;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MyCompany.MyProject.Models.ApiModels.Response.ResponseData
{
    public class GetClassResponseData
    {
        [JsonPropertyName("class_list")]
        public List<ClassResponseItem> ClassList { get; set; }
    }
}
