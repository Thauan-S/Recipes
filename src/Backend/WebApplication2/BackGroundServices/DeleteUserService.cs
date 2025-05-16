using Azure.Messaging.ServiceBus;
using Tropical.Application.UseCases.User.Delete.Delete;
using Tropical.Infrastructure.Services.ServiceBus;

namespace Tropical.API.BackGroundServices
{
    public class DeleteUserService : BackgroundService
    {   // NÃO ESQUECER DE ADICIONAR EM MEU PROGRAM.CS
        private readonly IServiceProvider _services;
        private readonly ServiceBusProcessor _processor;
       

        public DeleteUserService(DeleteUserProcessor processor, IServiceProvider services)
        {
            _processor = processor.GetProcessor(); // observe a injeção de dependências em infra
            _services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            
            _processor.ProcessMessageAsync += ProcessMessageAsync; // executa a func sempre q recebe uma msg
            _processor.ProcessErrorAsync += ExceptionReceivedHandler;// observe o atributo do processor ErrorAsync
                                                                     //// e o parametro da ExceptionReceivedHandler;
            await _processor.StartProcessingAsync(stoppingToken);
        }
        private async Task ProcessMessageAsync(ProcessMessageEventArgs eventArgs)
        {
            
            var message = eventArgs.Message.Body.ToString();

            var userIdentifier = Guid.Parse(message);
            var scope = _services.CreateScope();
            // obtem a referência da interface
            var deleteUserUseCase = scope.ServiceProvider.GetRequiredService<IDeleteUserAccountUseCase>();
            // devo adicionar ao program o hostprovider

            await deleteUserUseCase.Execute(userIdentifier);
        }
        private static Task ExceptionReceivedHandler(ProcessErrorEventArgs ex)
        {
            Console.WriteLine(ex.Exception);
            return Task.CompletedTask;
        }
        /// <summary>
        /// O ~ é obrigatório
        /// </summary>
        ~DeleteUserService() => Dispose();
        public override void Dispose()
        { // libera recursos na memória 

            base.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
