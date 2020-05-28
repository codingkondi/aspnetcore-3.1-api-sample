using MyCompany.MyProject.Models.ApiModels.Item;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MyCompany.MyProject.Models.ApiModels.Response.ResponseData
{
    public class GetStudentResponseData
    {
        [JsonPropertyName("student_list")]
        public List<StudentResponseItem> StudentList { get; set; }
    }
}
