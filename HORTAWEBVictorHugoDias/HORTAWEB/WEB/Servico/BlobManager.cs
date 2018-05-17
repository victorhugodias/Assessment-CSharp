using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB.Servico
{
    public class BlobManager
    {
        private CloudStorageAccount mAccount;
        private CloudBlobClient mBlobClient;
        public BlobManager()
        {
            mAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("BlobStorage"));
            mBlobClient = mAccount.CreateCloudBlobClient();
        }

        public BlobManager(CloudStorageAccount account)
        {
            mAccount = account;
            mBlobClient = mAccount.CreateCloudBlobClient();
        }

        public CloudBlobContainer CreateContainer(string containerName, bool isPublic)
        {
            CloudBlobContainer container = mBlobClient.GetContainerReference(containerName);

            container.CreateIfNotExists();

            if (isPublic)
            {
                container.SetPermissions(new BlobContainerPermissions() { PublicAccess = BlobContainerPublicAccessType.Blob });
            }

            return container;
        }

        public void SaveFileAsBlob(CloudBlobContainer container, string filePath, string blobName)
        {
            CloudBlockBlob blob = container.GetBlockBlobReference(blobName);

            using (var fileStream = System.IO.File.OpenRead(filePath))
            {
                blob.UploadFromStream(fileStream);
            }
        }

        public void SaveFileAsBlob(string containerName, string filePath, string blobName)
        {

            CloudBlobContainer container = mBlobClient.GetContainerReference(containerName);

            SaveFileAsBlob(container, filePath, blobName);
        }
    }
}