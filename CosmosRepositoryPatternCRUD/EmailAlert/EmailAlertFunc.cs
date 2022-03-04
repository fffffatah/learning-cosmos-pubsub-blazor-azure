using CosmosRepositoryPatternCRUD.Models;
using CosmosRepositoryPatternCRUD.Properties;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EmailAlert
{
    public static class EmailAlertFunc
    {
        private static readonly SendGridClient _sendGridClient =
            new SendGridClient(Environment.GetEnvironmentVariable("SendGridApiKey"));

        private static readonly string _fromEmailAddress =
            Environment.GetEnvironmentVariable("FromEmailAddress");

        private static readonly string _fromName =
            Environment.GetEnvironmentVariable("FromName");

        private static readonly string _toEmailAddress =
            Environment.GetEnvironmentVariable("ToEmailAddress");


        [FunctionName("EmailAlert")]
        public static async Task EmailAlert(
            [CosmosDBTrigger(
                databaseName: Constants.DatabaseName,
                collectionName: Constants.ContainerName,
                ConnectionStringSetting = "CosmosDbConnectionString",
                LeaseCollectionName = "leases",
            CreateLeaseCollectionIfNotExists = true)]
            IReadOnlyList<Document> documents,
            ILogger logger)
        {
            foreach (var document in documents)
            {
                try
                {
                    await CheckUpdateBook(document, logger);
                }
                catch (Exception ex)
                {
                    logger.LogError($"Error processing document id {document.Id}: {ex.Message}");
                }
            }
        }

        private static async Task CheckUpdateBook(Document document, ILogger logger)
        {
            var book = JsonConvert.DeserializeObject<Book>(document.ToString());
            Console.WriteLine(book.Genre.ToString());
            // await SendEmail(book, logger);
        }
        private static async Task SendEmail(Book book, ILogger logger)
        {
            var from = new EmailAddress(_fromEmailAddress, _fromName);
            var to = new EmailAddress(_toEmailAddress);
            var subject = $"Book {book.Title} has updated";
            var details = JsonConvert.SerializeObject(book, Formatting.Indented).Split(Environment.NewLine)
                .Where(d => d.Trim() != "{" && d.Trim() != "}");

            var detailsHtml = "<table>" + string.Join(null, details) + "</table>";

            var body =
                $"You are receiving this notice because Book <b>{book.Title}</b> has updated<br /><br />" +
                detailsHtml;

            var message = MailHelper.CreateSingleEmail(from, to, subject, body, body);

            var response = await _sendGridClient.SendEmailAsync(message);

            if (response.StatusCode == HttpStatusCode.Accepted)
            {
                logger.LogWarning(subject);
                return;
            }

            throw new Exception($"Error sending email to '{_toEmailAddress}'");
        }
    }
}
