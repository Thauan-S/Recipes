
using Azure.Messaging.ServiceBus;
using Tropical.Domain.Entities;
using Tropical.Domain.Services.ServiceBus;

namespace Tropical.Infrastructure.Services.ServiceBus
{
    public class DeleteUserQueue : IDeleteUserQueue
    {
        private readonly ServiceBusSender _serviceBusSender;

        public DeleteUserQueue(ServiceBusSender serviceBusSender)
        {
            _serviceBusSender = serviceBusSender;
        }

        public async Task SendMessage(User user)
        {  
           await _serviceBusSender.SendMessageAsync(new ServiceBusMessage(user.UserId.ToString()));
        }
    }
}
