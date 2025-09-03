using System.Net;
using System.Net.Mail;

namespace TechritizeAuthAPI.Services
{
    public class EmailService
    {
        private readonly string _senderEmail;
        private readonly string _appPassword;

        public EmailService(IConfiguration configuration)
        {
            _senderEmail = configuration["EmailSettings:SenderEmail"];
            _appPassword = configuration["EmailSettings:AppPassword"];
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            toEmail = toEmail?.Trim();
            if (string.IsNullOrWhiteSpace(toEmail))
                throw new ArgumentException("Recipient email cannot be null or empty.", nameof(toEmail));

            if (string.IsNullOrWhiteSpace(_senderEmail))
                throw new InvalidOperationException("Sender email is not configured.");

            using var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(_senderEmail, _appPassword),
                EnableSsl = true
            };

            using var mail = new MailMessage
            {
                From = new MailAddress(_senderEmail, "Techritize Auth System"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mail.To.Add(toEmail);

            await client.SendMailAsync(mail);
        }


    }
}
