using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.IO;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;

namespace app_HackSpace
{
    //-----AZURE STORAGE-------------------------------------------------------------------------------
    public class BlobServices
    {
        public CloudBlobContainer GetCloudBlobContainer()
        {
            string connString = "DefaultEndpointsProtocol=https;AccountName=hackspacestorage;AccountKey=y/aMBDs5qK1o5I2AkUBwo1U6kI2+6zQGULy19ULDfZCtBrmSEoZ0Vvh7sQUuc8vUYAN93rxluI9AIx/Ug8t5Hw==";
            string destContainer = "hackspacestorage";

            // Get a reference to the storage account  
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer blobContainer = blobClient.GetContainerReference(destContainer);
            if (blobContainer.CreateIfNotExists())
            {
                blobContainer.SetPermissions(new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                });
            }
            return blobContainer;
        }
    }

    public class BlobServicesDeliver
    {
        public CloudBlobContainer GetCloudBlobContainer()
        {
            string connString = "DefaultEndpointsProtocol=https;AccountName=hackspacestorage;AccountKey=y/aMBDs5qK1o5I2AkUBwo1U6kI2+6zQGULy19ULDfZCtBrmSEoZ0Vvh7sQUuc8vUYAN93rxluI9AIx/Ug8t5Hw==";
            string destContainer = "deliver";

            // Get a reference to the storage account  
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer blobContainer = blobClient.GetContainerReference(destContainer);
            if (blobContainer.CreateIfNotExists())
            {
                blobContainer.SetPermissions(new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                });
            }
            return blobContainer;
        }
    }

    public class BlobServicesExplore
    {
        public CloudBlobContainer GetCloudBlobContainer()
        {
            string connString = "DefaultEndpointsProtocol=https;AccountName=hackspacestorage;AccountKey=y/aMBDs5qK1o5I2AkUBwo1U6kI2+6zQGULy19ULDfZCtBrmSEoZ0Vvh7sQUuc8vUYAN93rxluI9AIx/Ug8t5Hw==";
            string destContainer = "explore";

            // Get a reference to the storage account  
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer blobContainer = blobClient.GetContainerReference(destContainer);
            if (blobContainer.CreateIfNotExists())
            {
                blobContainer.SetPermissions(new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                });
            }
            return blobContainer;
        }
    }

    public class BlobServicesLaunch
    {
        public CloudBlobContainer GetCloudBlobContainer()
        {
            string connString = "DefaultEndpointsProtocol=https;AccountName=hackspacestorage;AccountKey=y/aMBDs5qK1o5I2AkUBwo1U6kI2+6zQGULy19ULDfZCtBrmSEoZ0Vvh7sQUuc8vUYAN93rxluI9AIx/Ug8t5Hw==";
            string destContainer = "launch";

            // Get a reference to the storage account  
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer blobContainer = blobClient.GetContainerReference(destContainer);
            if (blobContainer.CreateIfNotExists())
            {
                blobContainer.SetPermissions(new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                });
            }
            return blobContainer;
        }
    }

    public class BlobServicesPitch
    {
        public CloudBlobContainer GetCloudBlobContainer()
        {
            string connString = "DefaultEndpointsProtocol=https;AccountName=hackspacestorage;AccountKey=y/aMBDs5qK1o5I2AkUBwo1U6kI2+6zQGULy19ULDfZCtBrmSEoZ0Vvh7sQUuc8vUYAN93rxluI9AIx/Ug8t5Hw==";
            string destContainer = "pitch";

            // Get a reference to the storage account  
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer blobContainer = blobClient.GetContainerReference(destContainer);
            if (blobContainer.CreateIfNotExists())
            {
                blobContainer.SetPermissions(new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                });
            }
            return blobContainer;
        }
    }
}