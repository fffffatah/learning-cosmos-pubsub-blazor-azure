using CosmosRepositoryPatternCRUD.Models;
using CosmosRepositoryPatternCRUD.Properties;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataArchiveFunction
{
    public static class BookArchive
    {
        private static readonly Dictionary<string, DateTime> _timestamps =
            new Dictionary<string, DateTime>();

        private static readonly object _threadLock =
            new object();

        private static readonly CloudBlobContainer _blobContainer;
        static BookArchive()
        {
            var storageAccount = new CloudStorageAccount(
                new StorageCredentials(
                    Environment.GetEnvironmentVariable("StorageAccountName"),
                    Environment.GetEnvironmentVariable("StorageAccountKey")),
                useHttps: true);

            var blobClient = storageAccount.CreateCloudBlobClient();

            _blobContainer = blobClient.GetContainerReference(Environment.GetEnvironmentVariable("BlobContainerName"));
        }
		[FunctionName("DataArchival")]
		public static async Task DataArchival(
			[CosmosDBTrigger(
				databaseName: Constants.DatabaseName,
				collectionName: Constants.ContainerName,
				ConnectionStringSetting = "CosmosDbConnectionString",
				LeaseCollectionName = "lease",
				LeaseCollectionPrefix = "DataArchival-",
			CreateLeaseCollectionIfNotExists = true
			)]
			IReadOnlyList<Document> documents,
			ILogger logger)
		{
			foreach (var document in documents)
			{
				try
				{
					await ArchiveBookData(document, logger);
				}
				catch (Exception ex)
				{
					logger.LogError($"Error processing document id {document.Id}: {ex.Message}");
				}
			}
		}
		private static async Task ArchiveBookData(Document document, ILogger logger)
		{

			var book = JsonConvert.DeserializeObject<Book>(document.ToString());
			if (ShouldSkip(book))
			{
				return;
			}

			var blobName = $"{DateTime.UtcNow:yyyyMMdd-HHmmss}-{book.Title}-{book.Id}.json";
			var blob = _blobContainer.GetBlockBlobReference(blobName);
			var bytes = Encoding.ASCII.GetBytes(document.ToString());
			await blob.UploadFromByteArrayAsync(bytes, 0, bytes.Length);

			logger.LogWarning($"Archived '{blobName}' to blob storage");

		}
		private static bool ShouldSkip(Book book)
		{

			lock (_threadLock)
			{
				if (_timestamps.ContainsKey(book.Id))
				{
					if (DateTime.Now.Subtract(_timestamps[book.Id]).TotalSeconds < 15)
					{
						return true;
					}
					_timestamps[book.Id] = DateTime.Now;
				}
				else
				{
					_timestamps.Add(book.Id, DateTime.Now);
				}
			}

			return false;
		}
	}
}
