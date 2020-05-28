using Microsoft.AspNetCore.Http;
using MyCompany.MyProject.Models.ApiModels.Response;
using MyCompany.MyProject.Models.ApiModels.Response.Error;
using MyCompany.MyProject.Models.ApiModels.Response.ResponseData;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyCompany.MyProject.Service.Controllers.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync(JsonSerializer.Serialize(
                    new ApiResponse<EmptyResponseData>()
                    {
                        Error = new ApiError()
                        {
                            ErrorCode = (int)HttpStatusCode.InternalServerError,
                            Message = string.Format("{0}:{1}", ex.Source, ex.Message)
                        }
                    }));
            }
        }
    }
}
