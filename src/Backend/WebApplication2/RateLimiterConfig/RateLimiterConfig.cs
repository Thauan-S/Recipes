using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using Tropical.Comunication.Responses;

namespace Tropical.API.RateLimiterConfig
{
    public class RateLimiterConfig : IRateLimiterPolicy<string>
    {
        public Func<OnRejectedContext, CancellationToken, ValueTask>? OnRejected => async (context, cancellationToken) =>
        {
            context.HttpContext.Response.StatusCode=(int) StatusCodes.Status429TooManyRequests;
           await context.HttpContext.Response.WriteAsJsonAsync(new ResponseErrorJson("Limite de requisições atingido , tente novamente mais tarde"),cancellationToken);
        };

        public RateLimitPartition<string> GetPartition(HttpContext httpContext)
        {
           string clientIp=httpContext.Connection.RemoteIpAddress.ToString();

            return RateLimitPartition.GetFixedWindowLimiter(clientIp, _ => new
            FixedWindowRateLimiterOptions
            {
                PermitLimit = 2,
                QueueLimit = 0,
                Window = TimeSpan.FromMinutes(5)
            });
        }
    }
}
