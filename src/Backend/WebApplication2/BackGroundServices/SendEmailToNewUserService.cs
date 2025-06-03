using Azure.Messaging.ServiceBus;
using Tropical.Application.UseCases.Email;
using Tropical.Infrastructure.Services.ServiceBus;

namespace Tropical.API.BackGroundServices
{
    public class SendEmailToNewUserService:BackgroundService
    {
        private readonly IServiceProvider _services;
        private readonly ServiceBusProcessor _processor;
        private readonly ILogger<SendEmailToNewUserService> _logger;

        public SendEmailToNewUserService(IServiceProvider services, SendEmailUserProccessor processor, ILogger<SendEmailToNewUserService> logger)
        {
            _services = services;
            _processor = processor.GetProcessor();
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _processor.ProcessMessageAsync += SendMailAsync;
            _processor.ProcessErrorAsync += ExceptionReceivedHandler;
            await _processor.StartProcessingAsync(stoppingToken);
        }
        private async Task SendMailAsync(ProcessMessageEventArgs eventArgs)
        {
            var email= eventArgs.Message.Body.ToString();
            var scope=_services.CreateScope();
            var sendMailUseCase = scope.ServiceProvider.GetRequiredService<ISendEmailUserUseCase>();
            await sendMailUseCase.SendEmailAsync(email);
        }
        private  Task ExceptionReceivedHandler(ProcessErrorEventArgs args)
        {
            var exeption = args.Exception;
            _logger.LogError($"ERROOOOOOOOOOOOOOR : {exeption.Message}");
            return Task.CompletedTask;
        }
        ~SendEmailToNewUserService() => Dispose();
        public override void Dispose()
        { // libera recursos na memória 

            base.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
