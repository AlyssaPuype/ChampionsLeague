using ChampionsLeague.Util.Mail.Interfaces;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionsLeague.Util.Mail
{
    public class IdentityEmailSend : IEmailSender
    {
        private readonly IEmailSend _emailSend;
        public IdentityEmailSend(IEmailSend emailSend)
        {
            _emailSend = emailSend;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            await _emailSend.SendEmailAsync(email, subject, htmlMessage);
        }
    }
}
