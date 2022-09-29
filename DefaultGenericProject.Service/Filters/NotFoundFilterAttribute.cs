using DefaultGenericProject.Core.DTOs.Responses;
using DefaultGenericProject.Core.Models;
using DefaultGenericProject.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;

namespace DefaultGenericProject.Service.Filters
{
    public class NotFoundFilterAttribute<T> : IAsyncActionFilter where T : BaseEntity
    {
        private readonly IGenericService<T> _genericService;

        public NotFoundFilterAttribute(IGenericService<T> genericService)
        {
            _genericService = genericService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var idValue = context.ActionArguments.Values.FirstOrDefault();
            if (idValue == null)
            {
                await next.Invoke();
                return;
            }

            var id = idValue.ToString();
            var anyEntity = await _genericService.AnyAsync(x => x.Id.ToString() == id);
            if (anyEntity)
            {
                await next.Invoke();
                return;
            }

            context.Result = new NotFoundObjectResult(Response<NoDataDTO>.Fail($"{typeof(T).Name}({id}) Not Found", 404, true));
        }
    }
}
