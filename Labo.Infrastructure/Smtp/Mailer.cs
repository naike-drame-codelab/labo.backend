using Labo.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;

namespace Labo.Infrastructure.Smtp
{
    public class Mailer(SmtpClient smtpClient, IConfiguration configuration)
        : IMailer
    {
        public void Send(string dest, string subject, string body, params Attachment[] attachments)
        {
            MailMessage message = new()
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };
            message.To.Add(new MailAddress(dest));
            message.From = new MailAddress("noreply@chess.com");
            // message.From = new MailAddress(configuration["Smtp:Username"] ?? throw new Exception("Missing smtp config"));

            foreach (var attachment in attachments) { 
                message.Attachments.Add(attachment);
            }

            smtpClient.Send(message);
        }
    }
}
