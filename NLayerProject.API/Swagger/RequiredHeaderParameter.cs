using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IOperationFilter = Swashbuckle.AspNetCore.SwaggerGen.IOperationFilter;
namespace NLayerProject.API.Swagger
{
    public class RequiredHeaderParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null) operation.Parameters = new List<OpenApiParameter>();

            var descriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;

            if (descriptor != null)
            {
                operation.Parameters.Add(new OpenApiParameter()
                {
                    Name = "ParameterName",
                    In = ParameterLocation.Header,
                    Description = "Parameter Description",
                    Required = true
                });
            }

        }
    }
}
