using System.Net.Mail;

namespace Labo.Application.Interfaces
{
    public interface IMailer
    {
        void Send(string dest, string subject, string body, params Attachment[] attachments);
    }
}
