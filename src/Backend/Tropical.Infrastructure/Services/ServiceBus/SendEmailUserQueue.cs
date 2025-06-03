using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tropical.Domain.Services.ServiceBus;

namespace Tropical.Infrastructure.Services.ServiceBus
{
    public class SendEmailUserQueue : ISendEmailUserQueue
    {
        private readonly ServiceBusSender _serviceBusSender;

        public SendEmailUserQueue(ServiceBusSender serviceBusSender)
        {
            _serviceBusSender = serviceBusSender;
        }

        public  async Task SendMessage(string email)
        {
            await _serviceBusSender.SendMessageAsync(new ServiceBusMessage(email));
        }
    }
}
