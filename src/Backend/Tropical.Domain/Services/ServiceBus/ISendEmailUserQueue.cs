

namespace Tropical.Domain.Services.ServiceBus
{
    public interface ISendEmailUserQueue
    {
        Task SendMessage(string  email);
    }
}
