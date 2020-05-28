using Microsoft.AspNetCore.Builder;

namespace MyCompany.MyProject.Service.Controllers.Middleware
{
    public class MiddlewareUse<T> where T : class
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<T>();
        }
    }
}
