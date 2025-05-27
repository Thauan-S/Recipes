using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sqids;
using Tropical.Application.Services.autoMapper;
using Tropical.Application.UseCases.Login.DoLogin;
using Tropical.Application.UseCases.Login.ExternalLogin;
using Tropical.Application.UseCases.Recipe;
using Tropical.Application.UseCases.Recipe.DashBoard;
using Tropical.Application.UseCases.Recipe.Delete;
using Tropical.Application.UseCases.Recipe.Filter;
using Tropical.Application.UseCases.Recipe.Generation;
using Tropical.Application.UseCases.Recipe.GetById;
using Tropical.Application.UseCases.Recipe.Register;
using Tropical.Application.UseCases.Recipe.UpdateById;
using Tropical.Application.UseCases.Recipe.UpdateImageById;
using Tropical.Application.UseCases.User.ChangePassword;
using Tropical.Application.UseCases.User.Delete.Delete;
using Tropical.Application.UseCases.User.Delete.RequestDeleteUseCase;
using Tropical.Application.UseCases.User.Profile;
using Tropical.Application.UseCases.User.Register;
using Tropical.Application.UseCases.User.Update;
using Tropical.Domain.Services.ServiceBus;
//using Tropical.Domain.Services.OpenAI;

namespace Tropical.Application
{
    public static class DependencyInjectionExtension
    {                                        
        public static void AddAplication(this IServiceCollection services, IConfiguration configuration)
        {                 
            AddAutoMapper(services);
            AddIdEncoder(services, configuration);
            AddUseCases(services);
        }
        private static void AddAutoMapper(IServiceCollection services)
        {
            services.AddScoped(option =>
                new AutoMapper.MapperConfiguration(autoMapperOptions =>
                {
                    var squIds = option.GetService<SqidsEncoder<long>>()!;
                    autoMapperOptions.AddProfile(new AutoMapping(squIds));

                }).CreateMapper());
        }
        private static void AddUseCases(IServiceCollection services)
        {
            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
            services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
            services.AddScoped<IGetUserProfileUseCase, GetProfileUseCase>();
            services.AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>();
            services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
            services.AddScoped<IRequestDeleteUserUseCase, RequestDeleteUserUseCase>();
            services.AddScoped<IDeleteUserAccountUseCase, DeleteUserAccountUseCase>();

            services.AddScoped<IExternalLoginUseCase, ExternalLoginUseCase>();


            services.AddScoped<IGetRecipeByIdUseCase, GetRecipeByIdUseCase>();
            services.AddScoped<IRegisterRecipeUseCase, RegisterRecipeUseCase>();
            services.AddScoped<IFilterRecipeUseCase, FilterRecipeUseCase>();
            services.AddScoped<IUpdateRecipeUseCase, UpdateRecipeUseCase>();
            services.AddScoped<IDeleteRecipeUseCase, DeleteRecipeUseCase>();

            services.AddScoped<IGetDashBoardUseCase, GetDashBoardUseCase>();
            services.AddScoped<IAddUpdateImageCoverUseCase, AddUpdateImageCoverUseCase>();
            ///TODO add integraçao com chat gpt
            

        }
        private static void AddIdEncoder(IServiceCollection services, IConfiguration configuration)
        {
            
            var squIds = new SqidsEncoder<long>(new()
            {  
                MinLength = 3,
                Alphabet = configuration.GetValue<string>("Settings:IdCriptografphyAlphabet")!
                // IdCriptografphyAlphabet generation =  https://github.com/sqids/sqids-dotnet
            });

            services.AddSingleton(squIds);

        }

    }
}
