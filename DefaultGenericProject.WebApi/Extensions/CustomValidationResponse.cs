using DefaultGenericProject.Core.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace DefaultGenericProject.WebApi.Extensions
{
    public static class CustomValidationResponse
    {
        public static void AddCustomValidationResponse(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState.Values.Where(x => x.Errors.Count > 0).SelectMany(x => x.Errors).Select(x => x.ErrorMessage);

                    ErrorDTO errorDTO = new(errors.ToList(), true);

                    var response = Response<NoContentResult>.Fail(errorDTO, 400);

                    return new BadRequestObjectResult(response);
                };
            });
        }
    }
}