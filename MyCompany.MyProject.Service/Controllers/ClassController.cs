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
    public class ClassController : ControllerBase
    {
        private readonly IClassLogic logic;
        public ClassController(IClassLogic classLogic)
        {
            logic = classLogic;
        }
        // GET: api/Class
        [HttpGet]
        public IActionResult GetClass()
        {
            ApiResponse<GetClassResponseData> response = new ApiResponse<GetClassResponseData>() { Data = new GetClassResponseData() };
            response.Data.ClassList = logic.GetClasses().MapToList<ClassVM, ClassResponseItem>().ToList();
            return Ok(response);
        }

        // GET: api/Class/id
        [HttpGet("{id}")]
        public IActionResult GetClass(int id)
        {
            ApiResponse<ClassResponseItem> response = new ApiResponse<ClassResponseItem>();
            ClassVM result = logic.GetClasses(id).FirstOrDefault();
            response.Data = result?.MapToItem<ClassVM, ClassResponseItem>();
            return Ok(response);
        }

        // POST: api/Class
        [HttpPost]
        public IActionResult CreateClass([FromQuery] string name)
        {
            ApiResponse<ClassResponseItem> response = new ApiResponse<ClassResponseItem>
            {
                Data = logic.CreateClass(name).MapToItem<ClassVM, ClassResponseItem>()
            };
            return Ok(response);
        }

        // PUT: api/Class/id
        [HttpPut]
        public IActionResult UpdateClass([FromQuery] int id, [FromQuery] string name)
        {
            ApiResponse<ClassResponseItem> response = new ApiResponse<ClassResponseItem>
            {
                Data = logic.UpdateClass(id, name).MapToItem<ClassVM, ClassResponseItem>()
            };
            return Ok(response);
        }

        // DELETE: api/ApiWithActions/id
        [HttpDelete("{id}")]
        public IActionResult DeleteClass(int id)
        {
            ApiResponse<DeleteResponseData> response = new ApiResponse<DeleteResponseData>() { Data = new DeleteResponseData() };
            response.Data.IsDeleted = logic.DeleteClass(id);
            return Ok(response);
        }
    }
}
