using Azure.Messaging.ServiceBus;
using Tropical.Application.UseCases.User.Delete.Delete;
using Tropical.Infrastructure.Services.ServiceBus;

namespace Tropical.API.BackGroundServices
{
    public class DeleteUserService : BackgroundService
    {   
        /// <summary>
        /// Injetada no program.cs
        /// </summary>
        private readonly IServiceProvider _services;
        private readonly ServiceBusProcessor _processor;
        private   readonly ILogger<DeleteUserService> _logger;

        public DeleteUserService(DeleteUserProcessor processor, IServiceProvider services, ILogger<DeleteUserService> logger)
        {
            _processor = processor.GetProcessor(); // observe a injeção de dependências em infra
            _services = services;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            
            _processor.ProcessMessageAsync += ProcessMessageAsync; // executa a func sempre q recebe uma msg
            _processor.ProcessErrorAsync += ExceptionReceivedHandler;// ErrorAsync
                                                                     //// e o parametro da ExceptionReceivedHandler;
            await _processor.StartProcessingAsync(stoppingToken);
        }
        private async Task ProcessMessageAsync(ProcessMessageEventArgs eventArgs)
        {
            
            var message = eventArgs.Message.Body.ToString();
            
            var userIdentifier = Guid.Parse(message);
            var scope = _services.CreateScope();
            // obtém a referência da interface
            var deleteUserUseCase = scope.ServiceProvider.GetRequiredService<IDeleteUserAccountUseCase>();
            

            await deleteUserUseCase.Execute(userIdentifier);
        }
        private  Task ExceptionReceivedHandler(ProcessErrorEventArgs args)
        {
              var exeption = args.Exception;

    _logger.LogError(exeption, 
        @"Erro ao processar mensagem no Service Bus. 
        Namespace: {Namespace}
        Entity: {EntityPath}
        Operation: {ErrorSource}
        Detalhes da exceção: {ExceptionMessage}",
        args.FullyQualifiedNamespace,
        args.EntityPath,
        args.ErrorSource,
        exeption.Message);
            _logger.LogError($"ERROOOOOOOOOOOOOO logado : {exeption.Message}");
            _logger.LogError($"ERROOOOOOOOOOOOO logado: {exeption.InnerException.Message}");
            _logger.LogError($"ERROOOOOOOOOOOOO logado: {exeption.InnerException}");

            Console.WriteLine(exeption.Message);
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
