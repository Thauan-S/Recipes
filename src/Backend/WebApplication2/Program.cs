using Tropical.API.Converters;
using Tropical.API.Middleware;
using Tropical.Application;
using Tropical.Exceptions.Filters;
using Tropical.Infrastructure;
using Microsoft.OpenApi.Models;
using Tropical.Domain.Security.Tokens;
using Tropical.API.Token;
using Tropical.API.Filters;
//using Tropical.API.BackGroundServices;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Configuration;
using Elastic.Apm.NetCoreAll;
using Tropical.Infrastructure.Extensions;
using Tropical.API.BackGroundServices;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Tropical.Infrastructure.Data;
using Tropical.API.RateLimiterConfig;
using Microsoft.AspNetCore.Cors.Infrastructure;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options => 
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
    });
builder.Services.AddMvc(options=>options.Filters.Add(typeof (ExceptionFilter)));


builder.Services.AddRateLimiter(options => options.AddPolicy<string, RateLimiterConfig>("ResetPasswordLimiter"));
//define todas as urls para min�sculo
builder.Services.AddRouting(options=>
options.LowercaseUrls=true);


// DI
builder.Services.AddAplication(builder.Configuration);

builder.Services.AddInfrastructure(builder.Configuration);

// adicionando backgroudService;
if ( !builder.Configuration.IsUnitTestEnvironment()) {
    //se n�o for um teste de intega��o
     builder.Services.AddHostedService<DeleteUserService>();
    builder.Services.AddHostedService<SendEmailToNewUserService>();
    AddGoogleAuthentication();
}
builder.Services.AddScoped<ITokenProvider,HttpContextTokenValue>();
builder.Services.AddHttpContextAccessor();



// Microsoft.AspNetCore.Authentication.JwtBearer
//habilitando healtechks para meu db no azure
builder.Services.AddHealthChecks()
    .AddDbContextCheck<AppDbContext>();

builder.Services.AddCors(options => options.AddPolicy("MyCorsPolicy", policy =>
{
    policy.WithOrigins("http://localhost:5173") 
          .AllowAnyHeader()  
          .AllowAnyMethod();                     
}));
var app = builder.Build();
//habilitando healtechks para minha api no azure
app.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    AllowCachingResponses = false,
    ResultStatusCodes = {
        [HealthStatus.Healthy]=StatusCodes.Status200OK,
        [HealthStatus.Unhealthy]=StatusCodes.Status503ServiceUnavailable,
    }
});
var serverUrls = builder.Configuration["ELASTIC_APM_SERVER_URLS"];
Console.WriteLine($"APM Server URL!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!: {serverUrls}");
Console.WriteLine($"ENVIRONMENT!!!!!!!: {app.Environment.EnvironmentName}");
app.UseAllElasticApm(builder.Configuration);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
}
app.UseMiddleware<CultureMiddleware>();
app.UseCors("MyCorsPolicy");
app.UseAuthentication();
app.UseAuthorization();
//deve ser chamado ap�s o useAuthorization, assim ele pode ler o contexto de autentica��o.
app.UseRateLimiter();
app.MapControllers();

app.Run();
void AddGoogleAuthentication()
{
    var clientId = builder.Configuration.GetValue<string>("Settings:Google:ClientId")!;
    var clientSecret = builder.Configuration.GetValue<string>("Settings:Google:ClientSecret")!;
    builder.Services.AddAuthentication(config =>
    {
        config.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;

    }).AddCookie()
    .AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = clientId;
        googleOptions.ClientSecret = clientSecret;
    });
    ///TODO ADD other 
}
public partial class Program
{

}