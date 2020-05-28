using Microsoft.AspNetCore.Http;
using MyCompany.MyProject.Models.ApiModels.Response;
using MyCompany.MyProject.Models.ApiModels.Response.Error;
using MyCompany.MyProject.Models.ApiModels.Response.ResponseData;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyCompany.MyProject.Service.Controllers.Middleware
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (await IsValidated(context))
                await _next(context);
        }

        private async Task<bool> IsValidated(HttpContext context)
        {
            string token = context.Request.Headers["api-token"];
            if (token is null)
            {
                await SetTokenInvalidResponse(context, "token is required!");
                return false;
            }

            if (!token.Equals("token_config"))
            {
                await SetTokenInvalidResponse(context, "token is invalid!");
                return false;
            }

            return true;
        }

        private async Task SetTokenInvalidResponse(HttpContext context, string message)
        {
            ApiResponse<EmptyResponseData> apiResponse = new ApiResponse<EmptyResponseData>()
            {
                Error = new ApiError() { ErrorCode = (int)HttpStatusCode.Unauthorized, Message = message }
            };
            byte[] result = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(apiResponse));
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            await context.Response.Body.WriteAsync(result, 0, result.Length);
        }
    }
}
