
using AutoMapper;
using Tropical.Comunication.Requests;
using Tropical.Comunication.Responses;
using Tropical.Comunication.Enums;
using Sqids;
using System.Text.Json;
using Newtonsoft.Json;

namespace Tropical.Application.Services.autoMapper
{
    public  class AutoMapping:Profile
    {
        private readonly SqidsEncoder<long> _idEncoder;
        public AutoMapping(SqidsEncoder<long>idEncoder)// classe do pacote sqids 
        {
            _idEncoder = idEncoder;
            RequestToDomain();
            DomainToResponse();
        }
        private void RequestToDomain()//Dto para entity
        {
            //  <Origem,                       Destino>// user está com Domain. pois ele não consegue reconhecer a classe em outro pacote
            CreateMap<RequestRegisterUserJson, Domain.Entities.User>()
                .ForMember(dest=>dest.Password,option=>option.Ignore());
            //ignorando  a senha 
            CreateMap<RequestRecipeJson, Domain.Entities.Recipe>()
                .ForMember(dest => dest.Instructions, opt => opt.Ignore())      //garantindo que não haja ingredientes duplicados
                .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(source => source.Ingredients.Distinct()))
                .ForMember(dest => dest.DishTypes, opt => opt.MapFrom(source => source.DishTypes.Distinct()));
            //mapeia a string para item de ingrediente
            CreateMap<string, Domain.Entities.Ingredient>()
                .ForMember(dest => dest.Item,opt=>opt.MapFrom(source=>source));
            
            CreateMap<DishType, Domain.Entities.DishType>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(source => source));

            CreateMap<RequestInstructionJson, Domain.Entities.Instruction>();
        }
        private void DomainToResponse()// entity para DTO
        {
            //  <Origem,                                   Destino>      
            CreateMap<Domain.Entities.User, ResponseUserProfileJson>();
            // criptografando o Id para devolucao do DTO 
            //TODO não esquecer de adicionar da injeção de dependência 
            CreateMap<Domain.Entities.Recipe,ResponseRegisteredRecipeJson>()
                .ForMember(dest=> dest.Id,config=> config.MapFrom(recipe=> _idEncoder.Encode(recipe.Id)));

            CreateMap<Domain.Entities.Recipe, ResponseShortRecipeJson>()
                .ForMember(dest => dest.Id, config => config.MapFrom(recipe => _idEncoder.Encode(recipe.Id)))
                .ForMember(dest => dest.AmountIngredients, config => config.MapFrom(recipe => recipe.Ingredients.Count));

            CreateMap<Domain.Entities.Recipe, ResponseRecipeJson>()
               .ForMember(dest => dest.Id, config => config.MapFrom(recipe => _idEncoder.Encode(recipe.Id)))
               .ForMember(dest => dest.DishTypes, config => config.MapFrom(recipe => recipe.DishTypes.Select(r=>r.Type)));

            CreateMap<Domain.Entities.Ingredient, ResponseIngredientJson>()
    .ForMember(dest => dest.Id, config => config.MapFrom(ingredient => _idEncoder.Encode(ingredient.Id)))
    .ForMember(dest => dest.Item, config => config.MapFrom(ingredient =>
        JsonConvert.DeserializeObject<IList<string>>(ingredient.Item) ?? new List<string>()
    ));
            CreateMap<Domain.Entities.Instruction, ResponseInstructionJson>()
            .ForMember(dest => dest.Id, config => config.MapFrom(instruction => _idEncoder.Encode(instruction.Id)));

        }

    }
}
