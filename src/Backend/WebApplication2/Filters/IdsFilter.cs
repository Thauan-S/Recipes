using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Tropical.API.Binders;

namespace Tropical.API.Filters
{
    public class IdsFilter : IOperationFilter
    {
        // HABILITA STRINGS NA ROTA DO SWAGGER nos ednpoints que  tiverem o model binder 
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var encyptedIds = context
                .ApiDescription
                .ParameterDescriptions
                .Where(x => x.ModelMetadata.BinderType == typeof(MyTropicalBinder))
                .ToDictionary(d=>d.Name,d=>d); 
           
            foreach (var parameter in operation.Parameters)
            {
                if (encyptedIds.TryGetValue(parameter.Name,out var apiParameter)) {
                    parameter.Schema.Format=string.Empty;
                    parameter.Schema.Type = "string";
                }
            }
            foreach(var schema in context.SchemaRepository.Schemas.Values)
            {
                foreach(var property in schema.Properties)
                {
                    if(encyptedIds.TryGetValue(property.Key,out var apiParameter)) {
                        property.Value.Format=string.Empty;
                        property.Value.Type = "string";
                    }
                }

            }
        }
    }
}
