using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tropical.Infrastructure.Services.ServiceBus
{
    public class SendEmailUserProccessor
    {
        private readonly ServiceBusProcessor _serviceBusProcessor;

        public SendEmailUserProccessor(ServiceBusProcessor serviceBusProcessor)
        {
            _serviceBusProcessor = serviceBusProcessor;
        }
        public ServiceBusProcessor GetProcessor() => _serviceBusProcessor;
    }
}
