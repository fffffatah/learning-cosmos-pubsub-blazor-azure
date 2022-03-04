using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeFeedDemo
{
    public class SendGridHelper
    {
        public static async Task Send(string from, string fromUserName, string to, string toUserName, string subject, string plainText, string html)
        {
            var client = new SendGridClient(Environment.GetEnvironmentVariable("SENDGRID_API"));
            var fromBuilder = new EmailAddress(from, fromUserName);
            var toBuilder = new EmailAddress(to, toUserName);
            var msg = MailHelper.CreateSingleEmail(fromBuilder, toBuilder, subject, plainText, html);
            await client.SendEmailAsync(msg);
        }
    }
}
