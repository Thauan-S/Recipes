using Tropical.Domain.Entities;

namespace Tropical.Domain.Services.ServiceBus
{
    public interface IDeleteUserQueue
    {
       Task SendMessage(User user);
    }
}
