using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel;

namespace API.Filter
{
    public class EnumOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                return;

            foreach (var parameter in operation.Parameters)
            {
                var parameterType = context.MethodInfo
                    .GetParameters()
                    .FirstOrDefault(p => p.Name == parameter.Name)?
                    .ParameterType;

                if (parameterType != null && parameterType.IsEnum)
                {
                    var enumDescriptions = Enum.GetValues(parameterType)
                        .Cast<Enum>()
                        .Select(e => GetEnumDescription(e))
                        .Select(desc => new Microsoft.OpenApi.Any.OpenApiString(desc))
                        .Cast<Microsoft.OpenApi.Any.IOpenApiAny>()
                        .ToList();

                    parameter.Schema.Enum = enumDescriptions;
                }
            }
        }

        private static string GetEnumDescription(Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var descriptionAttribute = fieldInfo?.GetCustomAttributes(typeof(DescriptionAttribute), false)
                .Cast<DescriptionAttribute>()
                .FirstOrDefault();
            return descriptionAttribute?.Description ?? value.ToString();
        }
    }
}
