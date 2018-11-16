using System;
using System.IO;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Web;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using EasyMobileApp.Models;

namespace EasyMobileApp.Services
{
    public class EasyBlobService
    {

        private CloudStorageAccount _cloudStorageAccount;
        private CloudBlobClient _cloudBlobClient;
        private string _containerNameString;
        private CancellationTokenSource _tokenSource;

        public EasyBlobService()
        {

            string connectionString = ConfigurationManager.AppSettings["STORAGE_CONNECTION_STRING"];
            string containerNameString = ConfigurationManager.AppSettings["BLOB_CONTAINER_NAME"];

            CloudStorageAccount.TryParse(connectionString, out _cloudStorageAccount);
           
            _cloudBlobClient = _cloudStorageAccount.CreateCloudBlobClient();
            _containerNameString = string.Copy(containerNameString);
            _tokenSource = new CancellationTokenSource();

        }

        public async Task<EasyBlobModel> DownloadBlobAsync(string imageNameString)
        {
          
            var containerReference = _cloudBlobClient.GetContainerReference(_containerNameString);
            var blobReference = containerReference.GetBlockBlobReference(imageNameString);

            byte[] blobBuffer = null;
            EasyBlobModel easyBlobModel = null;
            string blobExceptionMessage = string.Empty;
            string blobEncodedString = string.Empty;
                        
            using (var memoryStream = new MemoryStream())
            {
               
                try
                {

                    await blobReference.DownloadToStreamAsync(memoryStream, _tokenSource.Token);
                    blobBuffer = memoryStream.GetBuffer();
                    blobEncodedString = Convert.ToBase64String(blobBuffer, 0, blobBuffer.Length);

                }
                catch (Exception ex)
                {

                    blobExceptionMessage = ex.Message;


                }
                finally
                {

                    easyBlobModel = new EasyBlobModel()
                    {

                        BlobContents = blobEncodedString,
                        BlobName = imageNameString,
                        BlobExceptionMessage = blobExceptionMessage

                    };
                    
                }

                return easyBlobModel;


            }
            
        }

        public async Task<EasyBlobModel> UploadBlobAsync(string imageNameString, byte[] blobBytes)
        {

            var containerReference = _cloudBlobClient.GetContainerReference(_containerNameString);
            var blobReference = containerReference.GetBlockBlobReference(imageNameString);

            EasyBlobModel easyBlobModel = null;
            string blobExceptionMessage = string.Empty;

            try
            {

                await blobReference.UploadFromByteArrayAsync(blobBytes, 0, blobBytes.Length);                

            }
            catch (Exception ex)
            {

                blobExceptionMessage = ex.Message;

            }
            finally
            {

                easyBlobModel = new EasyBlobModel()
                {

                    BlobName = imageNameString,
                    BlobExceptionMessage = blobExceptionMessage

                };

            }

            return easyBlobModel;


        }

    }
}