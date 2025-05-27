using AutoMapper;
using Tropical.Comunication.Responses;
using Tropical.Domain.Entities;
using Tropical.Domain.Services.Storage;

namespace Tropical.Application.Extension
{
    public static class RecipeListExtension
    { //PROCESSAMENTO PARALELO
        public static async Task<IList<ResponseShortRecipeJson>> MapToShortRecipeJson(this IList<Recipe> recipes,
            User user, IBlobStorageService blobStorageService, IMapper mapper)
        {
           
            var result = recipes.Select(async recipe =>
            {
                var response = mapper.Map<ResponseShortRecipeJson>(recipe);
                if (recipe.ImageIdentifier != null)
                {                    
                    response.ImageUrl = await blobStorageService.GetFileUrl(user, recipe.ImageIdentifier);
                }
                
                return response; 
               
            });
            var response= await Task.WhenAll(result);
            return response;
            //var result = new List<ResponseShortRecipeJson>();
            //var result = recipes.Select()rec;
            ///TODO EXECUÇÃO PARALELA///
            //foreach (var recipe in recipes)
            //   var response= mapper.Map<ResponseShortRecipeJson>(recipe);
            //    if(recipe.ImageIdentifier != null)
            //    {                    // blobstorageService
            //      response.ImageUrl= await blobStorageService.GetFileUrl(user,recipe.ImageIdentifier);
            //    }
                
            //    result.Add(response);
            //}

           // return result;
        }
    }
}
