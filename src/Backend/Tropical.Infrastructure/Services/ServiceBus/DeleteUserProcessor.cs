﻿using Azure.Messaging.ServiceBus;

namespace Tropical.Infrastructure.Services.ServiceBus
{
    public class DeleteUserProcessor
    {
        private readonly ServiceBusProcessor _serviceBusProcessor;
       
        public DeleteUserProcessor(ServiceBusProcessor serviceBusProcessor)
        {
            _serviceBusProcessor = serviceBusProcessor;
        }

        public ServiceBusProcessor GetProcessor() =>_serviceBusProcessor;
     
    }
}
