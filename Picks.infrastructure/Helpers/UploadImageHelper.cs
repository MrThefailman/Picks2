using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Picks.core.Entities;
using Picks.infrastructure.Constants;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Picks.infrastructure.Helpers
{
    public class UploadImageHelper
    {
        public static bool IsImage(IFormFile file)
        {
            if (file.ContentType.Contains("image"))
            {
                return true;
            }

            string[] formats = { ".jpg", ".png", ".jpeg" };

            return formats.Any(item => file.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase));
        }

        public static async Task<Uri> UploadImage(
            IFormFile file, 
            string fileName, 
            string contentType, 
            AzureStorageConfig storageConfig)
        {
            try
            {
                IImageEncoder encoder = new JpegEncoder();
                IImageDecoder decoder = new JpegDecoder();

                if(fileName.EndsWith(".png", StringComparison.InvariantCultureIgnoreCase))
                {
                    encoder = new PngEncoder();
                    decoder = new PngDecoder();
                }

                var storageAccount = CloudStorageAccount.Parse(storageConfig.Connectionstring);

                // Blob in azure.
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference("uploads");
                CloudBlockBlob blockBlob = container.GetBlockBlobReference($"img/fullsize/{fileName}");
                blockBlob.Properties.ContentType = contentType;

                using (Stream fileStream = file.OpenReadStream())
                {
                    // Save the fullsize.
                    fileStream.Seek(0, SeekOrigin.Begin);
                    await blockBlob.UploadFromStreamAsync(fileStream);
                }
                
                using (WebClient client = new WebClient())
                {
                    client.Credentials = new NetworkCredential(CDNProfile.Username, CDNProfile.Password);
                    client.UploadFile(
                        "ftp://user_o2udupts@push-33.cdn77.com/www/Picks",
                        WebRequestMethods.Ftp.UploadFile,
                        $"{blockBlob.Uri}");
                }

                return blockBlob.Uri;
            }
            catch (Exception err)
            {
                return null;
            }
        }
    }
}
