using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Tropical.Comunication.Responses;
using Tropical.Exceptions.Exceptions;

namespace Tropical.Exceptions.Filters
{
    public class ExceptionFilter: IExceptionFilter
    { //TODO ESTOU FERINDO O PRINCÍPIO O DO SOLID , POIS TODA VEZ QUE TENHO UMA NOVA EXEÇÃO TENHO QUE MUDAR ESTA CLASSE
        //ADICIONANDO UM IF ELSE A MAIS
        public void OnException(ExceptionContext context)
        {
            if(context.Exception is MyTropicalException)// o tipo de context=System.error
                HandleProjectException(context);
            else
                ThrowUnknowException(context); 
        }
        private void HandleProjectException(ExceptionContext context)
        {
            if (context.Exception is InvalidLoginException)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(context.Exception.Message));
            }
            else if (context.Exception is ErrorOnValidationException)
            {
                var exception=context.Exception as ErrorOnValidationException;

                context.HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                                    // atenção ao tipo de Object retornado , pois mesmo alterando o statuscode
                                    //ele irá retornar o código do objeto abaixo
                context.Result = new BadRequestObjectResult(new ResponseErrorJson(exception!.ErrorMessages));
            }else if (context.Exception is NotFoundException) {
                context.HttpContext.Response.StatusCode= StatusCodes.Status404NotFound;
                context.Result = new NotFoundObjectResult(new ResponseErrorJson(context.Exception.Message));
            }
        }
        private void ThrowUnknowException(ExceptionContext context)// exceção padrão 500    
        {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Result = new ObjectResult(new ResponseErrorJson(ResourceMessagesException.UNKNOWN_ERROR));
        }
    }
}
