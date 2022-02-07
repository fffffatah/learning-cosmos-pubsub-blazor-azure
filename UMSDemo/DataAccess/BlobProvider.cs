using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class BlobProvider
    {
        private string _blobConnString;
        private string _containerName;

        public BlobProvider()
        {
            _blobConnString = Environment.GetEnvironmentVariable("BLOB_CONN_STRING");
            _containerName = Environment.GetEnvironmentVariable("BLOB_CONTAINER");
        }

        public async Task<string> UploadFileToBlobAsync(string strFileName, IBrowserFile file)
        {
            try
            {
                BlobServiceClient blobServiceClient = new BlobServiceClient(_blobConnString);
                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_containerName);
                BlobClient blobClient = containerClient.GetBlobClient(strFileName);
                using (var stream = file.OpenReadStream())
                {
                    await blobClient.UploadAsync(stream, true);
                }
                return blobClient.Uri.AbsoluteUri;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public async Task<bool> DeleteFromBlobAsync(string strFileName)
        {
            try
            {
                BlobServiceClient blobServiceClient = new BlobServiceClient(_blobConnString);
                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_containerName);
                BlobClient blobClient = containerClient.GetBlobClient(strFileName);
                await blobClient.DeleteAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
