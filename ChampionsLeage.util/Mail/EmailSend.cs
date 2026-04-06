using ChampionsLeage.util.Mail;
using ChampionsLeage.util.Mail.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ChampionsLeague.Util.Mail
{
    public class EmailSend : IEmailSend
    {
        private readonly EmailSettings _emailSettings;

        public EmailSend(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var mail = new MailMessage();
            mail.To.Add(new MailAddress(email));
            mail.From = new
            MailAddress("alyssar0956092@gmail.com");
            mail.Subject = subject;
            mail.Body = message;
            mail.IsBodyHtml = true;
            try
            {
                using (var smtp = new SmtpClient(_emailSettings.MailServer))
                {
                    smtp.Port = _emailSettings.MailPort;
                    smtp.EnableSsl = true;
                    smtp.Credentials =
                    new NetworkCredential(_emailSettings.Sender,
                    _emailSettings.Password);
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            { throw; }
        }
    }
}





