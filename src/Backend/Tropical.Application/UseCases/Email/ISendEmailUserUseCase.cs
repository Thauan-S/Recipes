
namespace Tropical.Application.UseCases.Email
{
    public interface ISendEmailUserUseCase
    {
        Task SendEmailAsync(string emailTo);
    }
}
