using Microsoft.AspNetCore.Mvc;
using MyCompany.MyProject.Extensions.AutoMapper;
using MyCompany.MyProject.Logic.Interface;
using MyCompany.MyProject.Models.ApiModels.Item;
using MyCompany.MyProject.Models.ApiModels.Response;
using MyCompany.MyProject.Models.ApiModels.Response.ResponseData;
using MyCompany.MyProject.Models.ViewModels;
using MyCompany.MyProject.Service.Controllers.Middleware;
using System.Linq;

namespace MyCompany.MyProject.Service.Controllers
{
    [Route("api/[controller]")]
    [MiddlewareFilter(typeof(MiddlewareUse<ExceptionMiddleware>))]
    [MiddlewareFilter(typeof(MiddlewareUse<AuthMiddleware>))]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentLogic logic;
        public StudentController(IStudentLogic StudentLogic)
        {
            logic = StudentLogic;
        }
        // GET: api/Student
        [HttpGet]
        public IActionResult GetStudent()
        {
            ApiResponse<GetStudentResponseData> response = new ApiResponse<GetStudentResponseData>() { Data = new GetStudentResponseData() };
            response.Data.StudentList = logic.GetStudents().MapToList<StudentVM, StudentResponseItem>().ToList();
            return Ok(response);
        }

        // GET: api/Student/id
        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
        {
            ApiResponse<StudentResponseItem> response = new ApiResponse<StudentResponseItem>();
            StudentVM result = logic.GetStudents(id).FirstOrDefault();
            response.Data = result?.MapToItem<StudentVM, StudentResponseItem>();
            return Ok(response);
        }

        // POST: api/Student
        [HttpPost]
        public IActionResult CreateStudent([FromQuery] string name)
        {
            ApiResponse<StudentResponseItem> response = new ApiResponse<StudentResponseItem>
            {
                Data = logic.CreateStudent(name).MapToItem<StudentVM, StudentResponseItem>()
            };
            return Ok(response);
        }

        // PUT: api/Student/id
        [HttpPut]
        public IActionResult UpdateStudent([FromQuery] int id, [FromQuery] string name)
        {
            ApiResponse<StudentResponseItem> response = new ApiResponse<StudentResponseItem>
            {
                Data = logic.UpdateStudent(id, name).MapToItem<StudentVM, StudentResponseItem>()
            };
            return Ok(response);
        }

        // DELETE: api/Student/id
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            ApiResponse<DeleteResponseData> response = new ApiResponse<DeleteResponseData>() { Data = new DeleteResponseData() };
            response.Data.IsDeleted = logic.DeleteStudent(id);
            return Ok(response);
        }
    }
}
