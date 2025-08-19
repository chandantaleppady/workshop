using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;
using System.Text;
using UtilsLib.Configs;

namespace UtilsLib.Tools
{
    /// <summary>
    /// Provides utility methods for interacting with Azure Blob Storage.
    /// </summary>
    public class BlobUtils : IBlobUtils
    {
        private readonly BlobConfigs _blobConfigs;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlobUtils"/> class with the specified blob configuration options.
        /// </summary>
        /// <param name="blobConfigs">The blob storage configuration options.</param>
        public BlobUtils(IOptions<BlobConfigs> blobConfigs)
        {
            _blobConfigs = blobConfigs.Value;
        }

        /// <summary>
        /// Uploads the specified content to a blob in the given container.
        /// </summary>
        /// <param name="content">The content to upload, represented as a <see cref="StringBuilder"/>.</param>
        /// <param name="filePath">The path of the file to upload within the blob storage.</param>
        /// <returns>
        /// A task representing the asynchronous operation, with a result indicating <c>true</c> if the upload was successful; otherwise, <c>false</c>.
        /// </returns>
        public Task<bool> UploadFileAsync(StringBuilder content, string filePath)
        {
            // upload the content to the specified blob storage
            try
            {
                var blobServiceClient = new BlobServiceClient(_blobConfigs.ConnectionString);
                var containerClient = blobServiceClient.GetBlobContainerClient(_blobConfigs.ContainerName);
                containerClient.CreateIfNotExists();

                var blbClient = containerClient.GetBlobClient(filePath);

                using var stream = new MemoryStream(Encoding.UTF8.GetBytes(content.ToString()));
                blbClient.Upload(stream, overwrite: true);

                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                return Task.FromResult(false);
            }
        }
    }
}
