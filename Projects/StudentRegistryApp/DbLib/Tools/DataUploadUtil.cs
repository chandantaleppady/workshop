using CSharpFunctionalExtensions;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace UtilsLib.Tools
{
    /// <summary>
    /// Provides utility methods for uploading data to blob storage in JSON format.
    /// </summary>
    public class DataUploadUtil : IDataUploadUtil
    {
        private readonly IBlobUtils _blobUtils;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataUploadUtil"/> class.
        /// </summary>
        /// <param name="blobUtils">The blob utility used for uploading files.</param>
        public DataUploadUtil(IBlobUtils blobUtils)
        {
            _blobUtils = blobUtils;
        }

        /// <summary>
        /// Serializes the provided records to JSON and uploads them to blob storage.
        /// </summary>
        /// <typeparam name="T">The type of the records to upload.</typeparam>
        /// <param name="records">The collection of records to serialize and upload.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating whether the upload was successful, with a message describing the outcome.
        /// </returns>
        public Result UploadData<T>(IEnumerable<T> records)
        {
            try
            {
                JsonSerializerOptions options = new()
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                    WriteIndented = true
                };

                StringBuilder data = new(JsonSerializer.Serialize(records, options));
                string fileName = $"DataUpload_{DateTime.Now:yyyyMMdd_HHmmss}.json";
                var uploadResult = _blobUtils.UploadFileAsync(data, fileName).Result;
                return uploadResult
                    ? Result.Success($"Data uploaded successfully to {fileName}.")
                    : Result.Failure("Failed to upload data.");
            }
            catch (Exception ex)
            {
                return Result.Failure($"An error occurred while uploading data: {ex.Message}");
            }
        }
    }
}
