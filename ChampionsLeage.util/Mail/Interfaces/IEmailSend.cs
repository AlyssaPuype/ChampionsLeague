using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionsLeague.Util.Mail.Interfaces
{
    public interface IEmailSend
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
