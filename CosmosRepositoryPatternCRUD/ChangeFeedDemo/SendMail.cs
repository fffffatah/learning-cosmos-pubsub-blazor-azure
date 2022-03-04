using System;
using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace ChangeFeedDemo
{
    public static class SendMail
    {
        [FunctionName("SendMail")]
        public static void Run([CosmosDBTrigger(
            databaseName: "fffffatah",
            collectionName: "books",
            ConnectionStringSetting = "CosmosString",
            LeaseCollectionName = "leases")]IReadOnlyList<Document> input,
            ILogger log)
        {
            if (input != null && input.Count > 0)
            {
                log.LogInformation("Documents modified " + input.Count);
                log.LogInformation("First document Id " + input[0].Id);
                SendGridHelper.Send("info@bs-23.com", "System", "ab.fatahmn@gmail.com", "Noorullah", "A Book Has Been Updated", "A Book Has Been Updated", "<b>A Book Has Been Updated</b>");
            }
        }
    }
}
