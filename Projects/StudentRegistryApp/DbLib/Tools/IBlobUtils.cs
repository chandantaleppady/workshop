using System.Text;

namespace UtilsLib.Tools
{
    /// <summary>
    /// Provides utility methods for interacting with blob storage.
    /// </summary>
    public interface IBlobUtils
    {
        /// <summary>
        /// Uploads a file to a blob storage.
        /// </summary>
        /// <param name="content">The content to upload, represented as a <see cref="StringBuilder"/>.</param>
        /// <param name="filePath">The path of the file to upload within the blob storage.</param>
        /// <returns>
        /// A task representing the asynchronous operation, with a result indicating <c>true</c> if the upload was successful; otherwise, <c>false</c>.
        /// </returns>
        Task<bool> UploadFileAsync(StringBuilder content, string filePath);
    }
}
