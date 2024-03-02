using BusinessLayer.ResquestModels;
using Microsoft.Extensions.Configuration;

using MailKit.Security;
using MimeKit.Text;
using MimeKit;

namespace BusinessLayer.Services.Implements
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        private string GenerateOtp(int length)
        {
            var randomGenerator = new Random();
            string otp = string.Empty;
            for (int i = 0; i < length; i++)
            {
                otp += randomGenerator.Next(0, 9).ToString();
            }
            return otp;
        }

        public bool SendEmail(EmailRequestModel request)
        {
            try
            {
                var emailBodyTemplate = _config.GetSection("EmailSetting")?["EmailBody"];
                var emailBody = emailBodyTemplate.Replace("{OTP_CODE}", GenerateOtp(6));

                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailSetting")?["EmailHost"]));
                email.To.Add(MailboxAddress.Parse(request.UserEmail));
                email.Subject = _config.GetSection("EmailSetting")?["Subject"];
                email.Body = new TextPart(TextFormat.Html)
                {
                    Text = emailBody
                };

                using var smtp = new MailKit.Net.Smtp.SmtpClient();
                smtp.Connect(_config.GetSection("EmailSetting")?["EmailHost"], 587, SecureSocketOptions.StartTls);
                smtp.Authenticate(_config.GetSection("EmailSetting")?["EmailUsername"], _config.GetSection("EmailSetting")?["EmailPassword"]);
                smtp.Send(email);
                smtp.Disconnect(true);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
