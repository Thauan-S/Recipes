﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tropical.Domain.Repositories;
using Tropical.Domain.Repositories.Recipe;
using Tropical.Domain.Repositories.User;
using Tropical.Domain.Security.Cryptography;
using Tropical.Domain.Security.Tokens;
using Tropical.Domain.Services.Storage;
using Tropical.Infrastructure.Data;
using Tropical.Infrastructure.Data.Repositories;
using Tropical.Infrastructure.Extensions;
using Tropical.Infrastructure.Security.Cryptography;
using Tropical.Infrastructure.Security.Tokens.Access.Generator;
using Tropical.Infrastructure.Security.Tokens.Access.validator;
using Tropical.Infrastructure.Services.LoggedUser;
using Tropical.Infrastructure.Services.Storage;
using Azure.Storage.Blobs;
using Tropical.Domain.Services.LoggedUser;
using Tropical.Infrastructure.Services.ServiceBus;
using Tropical.Domain.Services.ServiceBus;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
using Tropical.Infrastructure.Security.Tokens.Refresh;
using Tropical.Domain.Repositories.RefreshToken;
using Tropical.Infrastructure.Services.Caching;


namespace Tropical.Infrastructure
{
    public static class DependencyInjectionExtension
    {
       
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddTokens(services, configuration);
            AddInfrastructure(services);
            AddPasswordEncripter(services);
            AddRepositories(services);
            AddAzureStorage(services,configuration);
            AddServiceBus(services,configuration);
            if (configuration.IsUnitTestEnvironment())// adicionando testes de integração
            {
                return;
            }
            var connectionString = configuration.ConnectionString();
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString,
                
                options => options.MigrationsAssembly("Tropical.Infrastructure"));// cria as migrations a partir do propeto de infra
            });//Por padrão, o EF Core tenta colocar as Migrations no mesmo projeto onde o DbContext está.
            ////var serviceprovider = services.BuildServiceProvider();
            ////var db = serviceprovider.GetRequiredService<AppDbContext>();
            ////db.Database.Migrate();
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetValue<string>("Settings:Redis:Connection");
                options.InstanceName = "SampleInstance";
            });


        }

        private static void AddInfrastructure(IServiceCollection services)
            => services.AddScoped<ILoggedUser, LoggedUser>();

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUnityOfWork, UnityOfWork>();

            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
            services.AddScoped<IUserReadOnlyRepository, UserRepository>();
            services.AddScoped<IUserUpdateOnlyRepository, UserRepository>();
            services.AddScoped<IUserDeleteOnlyRepository, UserRepository>();
            

            services.AddScoped<IRecipeReadOnlyRepository, RecipeRepository>();
            services.AddScoped<IRecipeWriteOnlyRepository, RecipeRepository>();
            services.AddScoped<IRecipeUpdateOnlyRepository, RecipeRepository>();

            services.AddScoped<ITokenRepository, RefreshTokenRepository>();

            services.AddScoped<ICachingService, CacheService>();

        }
        private static void AddServiceBus(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString= configuration.GetValue<string>("Settings:ServiceBus:DeleteUserAccount");
             
            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetRequiredService<ILoggerFactory>()
                                        .CreateLogger("DependencyInjectionExtension");

            logger.LogInformation("Service Bus connection string!: {ConnectionString}", connectionString);

            if (string.IsNullOrWhiteSpace(connectionString))
            {// verifica testes de integração 
                return;
            }


            var client = new ServiceBusClient(connectionString, new ServiceBusClientOptions()
            {
                TransportType = ServiceBusTransportType.AmqpTcp,// service bus emulator não aceita websockets
            });
            // estou simulando o serviceBus Localmente no docker
            var deletequeue = new DeleteUserQueue(client.CreateSender("queue.1"));
            var emailqueue = new SendEmailUserQueue(client.CreateSender("emailqueue.1"));
            var sendEmailUserProcessor = new SendEmailUserProccessor(client.CreateProcessor("emailqueue.1",new ServiceBusProcessorOptions()
            {
                MaxConcurrentCalls=1,
            }));
            var deleteUserProcessor = new DeleteUserProcessor(client.CreateProcessor("queue.1", new ServiceBusProcessorOptions()
            {
                MaxConcurrentCalls = 1 ,//recebe apenas 1 mensagem e processa;
            }));
            services.AddSingleton(deleteUserProcessor);
            services.AddSingleton(sendEmailUserProcessor);
            services.AddScoped<IDeleteUserQueue>(options => deletequeue);
            services.AddScoped<ISendEmailUserQueue>(options => emailqueue);
        }
        private static void AddTokens(IServiceCollection services, IConfiguration configuration)
        {
            var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpirationTimeMinutes");
            var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");
            
            services.AddScoped<IAccessTokenGenerator>(option =>
            new JwtTokenGenerator(expirationTimeMinutes, signingKey!));

            //configuração do validator do token
            services.AddScoped<IAccessTokenValidator>(option =>
            new JwtTokenValidator(signingKey!));

            services.AddScoped<IRefreshTokenGenerator,RefreshTokenGenerator>();
           
        }
        private static void AddAzureStorage(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("Settings:BlobStorage:Azure");
            // perceba que na classe do AzureStorageService eu preciso do meu BlobServiceClient, e como é uma classe
            // devo instancia-lo aqui.
            if (!string.IsNullOrEmpty(connectionString))
            {// verifica se está realizando teste de integração 
                services.AddScoped<IBlobStorageService>(c => new AzureStorageService(new BlobServiceClient(connectionString)));
            }
           
        }
        private static void AddPasswordEncripter(IServiceCollection services)
        {
            services.AddScoped<IPasswordEncripter,PasswordEncripter>();
        }
    }
}
