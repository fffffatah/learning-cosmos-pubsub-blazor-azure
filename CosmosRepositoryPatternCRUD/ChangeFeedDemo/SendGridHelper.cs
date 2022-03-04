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
        public static void Send(string from, string fromUserName, string to, string toUserName, string subject, string plainText, string html)
        {
            var client = new SendGridClient(Environment.GetEnvironmentVariable("SENDGRID_API"));
            var fromBuilder = new EmailAddress(from, fromUserName);
            var subjectBuilder = subject;
            var toBuilder = new EmailAddress(to, toUserName);
            var plainTextContent = plainText;
            var htmlContent = html;
            var msg = MailHelper.CreateSingleEmail(fromBuilder, toBuilder, subjectBuilder, plainTextContent, htmlContent);
            var response = client.SendEmailAsync(msg);
        }
    }
}
