

using System.Net.Mail;
using System.Net;
using Tropical.Domain.ValueObjects;

namespace Tropical.Application.UseCases.Email
{
    public class SendEmailUserUseCase : ISendEmailUserUseCase
    {
        public readonly string _appKey;
        public readonly string _emailFrom;

        public SendEmailUserUseCase(string appKey, string email)
        {
            _appKey = appKey;
            _emailFrom = email;
        }

        public async  Task SendEmailAsync(string emailTo)
        {
          
            var client = new SmtpClient("smtp.gmail.com", 587)
            {   
                Credentials = new NetworkCredential(_emailFrom, _appKey),
                EnableSsl = true
            };

            await client.SendMailAsync(new MailMessage(_emailFrom, emailTo, "Boas vindas", "Seja bem vindo à plataforma"));
        }
    }
}
