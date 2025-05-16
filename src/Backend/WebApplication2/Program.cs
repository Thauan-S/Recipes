using Tropical.API.Converters;
using Tropical.API.Middleware;
using Tropical.Application;
using Tropical.Exceptions.Filters;
using Tropical.Infrastructure;
using Microsoft.OpenApi.Models;
using Tropical.Domain.Security.Tokens;
using Tropical.API.Token;
using Tropical.API.Filters;
using Tropical.API.BackGroundServices;
using Microsoft.AspNetCore.Authentication.Cookies;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options => // adiciona o converter para remover espaços em branco no metodo post do user
options.JsonSerializerOptions.Converters.Add(new StringConverter()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
    {
    options.OperationFilter<IdsFilter>(); // filtro para aceitar string na rota do getRecipebyId(criptografia de id)
        options.AddSecurityDefinition("Bearer",new OpenApiSecurityScheme
        {
            Description =@"JWT authorization header using Bearer scheme.
                           enter 'Bearer' [space] and then you tokern in the text input bellow
                            Eample: 'Bearer 12342342aewas'",
            Name="Authorization",
            In=ParameterLocation.Header,
            Type=SecuritySchemeType.ApiKey,
            Scheme="Bearer"
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
            new OpenApiSecurityScheme {
                Reference=new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                },
                Scheme="oauth2",
                Name="Bearer",
                In=ParameterLocation.Header
            },
            new List<string>()
            }
        });
    }); // configurando swagger para login
// passando o Exception filter nas dependências
builder.Services.AddMvc(options=>options.Filters.Add(typeof (ExceptionFilter)));

//define todas as urls para minúsculo
builder.Services.AddRouting(options=>
options.LowercaseUrls=true);


//ADICIONANDO DI
//passando a configuration para os Services
builder.Services.AddAplication(builder.Configuration);//As dependencias são injetadas em Dependency InjectionExtension

builder.Services.AddInfrastructure(builder.Configuration);

// adicionando minha classe que está executando em segundo plano;
builder.Services.AddHostedService<DeleteUserService>();
//
builder.Services.AddScoped<ITokenProvider,HttpContextTokenValue>();
builder.Services.AddHttpContextAccessor();

//preparando controllers para receber o token 
//Devo instalar Microsoft.AspNetCore.Authentication.JwtBearer
// e inserir o código aula 100, porém ele fez um  authorize personalizado
//Domain IAccessTokenValidator


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<CultureMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
//void AddGoogleAuthenticationOptions()
//{
//    var clientId = builder.Configuration.GetValue<string>("Settings:Google:ClientId");
//    var clientSecret = builder.Configuration.GetValue<string>("Settings:Google:ClientSecret");
//    builder.Services.AddAuthentication(config =>
//    {
//        config.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;

//    }).AddCookie().ad;
//}
public partial class Program
{

}