using AutoMapper.Internal;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CMS.Api.Filters
{
    public class SwaggerNullableParameterFilter : IParameterFilter
    {
        //filter de kiem tra nullable
        public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
        {
            if (!parameter.Schema.Nullable &&
                (context.ApiParameterDescription.Type.IsNullableType() || !context.ApiParameterDescription.Type.IsValueType))
            {
                parameter.Schema.Nullable = true;
            }
            //xac dinh kieu tra va nullable = true
            //      "schema": { "type": "string", "nullable": true }

        }
    }
}
