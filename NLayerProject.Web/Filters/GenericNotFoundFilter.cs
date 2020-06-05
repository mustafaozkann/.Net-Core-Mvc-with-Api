using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayerProject.Core.Services;
using NLayerProject.Web.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerProject.Web.Filters
{
    public class GenericNotFoundFilter<TEntity> : IAsyncActionFilter where TEntity : class
    {
        private readonly IService<TEntity> _service;

        public GenericNotFoundFilter(IService<TEntity> service)
        {
            _service = service;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            int id = (int)context.ActionArguments.Values.FirstOrDefault();
            var entry = await _service.GetByIdAsync(id);
            if (entry != null)
            {
                await next();
            }
            else
            {
                ErrorDto errorDto = new ErrorDto();
                errorDto.Status = 404;
                errorDto.Errors.Add($"Belirtmiş olduğunuz nesne veritabanında bulunamadı");
                context.Result = new RedirectToActionResult("Error", "Home", errorDto);
            }
        }
    }
}
