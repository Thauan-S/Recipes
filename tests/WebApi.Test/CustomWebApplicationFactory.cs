using CommonTestUtilities.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tropical.Infrastructure.Data;

namespace WebApi.Test
{
    //criar uma classe partitial em program.cs
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        private Tropical.Domain.Entities.User _user = default!;
        private string _password = string.Empty;


        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {//criando um appsettings para o ambiente de testes de integração
            //devo criar (copiar  colar) um novo appsetings.nome abaixo.json
            builder.UseEnvironment("Test")
                .ConfigureServices(services =>
                {
                    var descriptor = services
                    .SingleOrDefault
                    (   //verificando se já existe o appdbcontext 
                        d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
                    if (descriptor != null) services.Remove(descriptor);
                    //configurando bd em memoria
                    var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();
                    services.AddDbContext<AppDbContext>(options =>
                    {                               //deve ser exatamente esse nome
                        options.UseInMemoryDatabase("InMemoryDbForTesting");
                        options.UseInternalServiceProvider(provider);
                    });

                    //criando testes de integração para o login
                    using var scope = services.BuildServiceProvider().CreateScope();

                    var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    appDbContext.Database.EnsureDeleted(); // garante que o db não exista,se existir deleta

                    StartDataBase(appDbContext);
                });
        }
        public string GetEmail() => _user.Email;
        public string GetPassword() => _password;
        public string GetName() => _user.Name;
        private void StartDataBase(AppDbContext appDbContext)
        {
            //criar testes de integração para o login
            (_user, _password) = UserBuilder.Build();
            appDbContext.Users.Add(_user);
            appDbContext.SaveChangesAsync();
        }
    }
}
