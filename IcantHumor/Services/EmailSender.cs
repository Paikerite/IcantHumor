using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Net;

namespace IcantHumor.Services
{
    public class EmailSender : IEmailSender
    {
        private MailAddress EmailFrom;
        public EmailSender()
        {
            if (MailAddress.TryCreate("cheekibreeki1109@gmail.com","IcantHumor", out var result))
            {
                this.EmailFrom = result;
            }
            else
            {
                throw new Exception("Failed to create sender MailAddress in EmailSender");
            }
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MailAddress EmailTo;
            if (MailAddress.TryCreate(email, subject, out var result))
            {
                EmailTo = result;
            }
            else
            {
                throw new Exception("Failed to create receiver MailAddress in EmailSender");
            }

            SmtpClient client = new SmtpClient
            {
                Port = 587,
                Host = "smtp.gmail.com", // или другой поставщик отправки электронной почты 
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("cheekibreeki1109@gmail.com", "xysi ymyg yjtr true")
            };

            MailMessage mailMessage = new MailMessage(EmailFrom, EmailTo);
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = htmlMessage;
            mailMessage.Subject = subject;

            return client.SendMailAsync(mailMessage);
        }
    }
}
