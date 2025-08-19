namespace UtilsLib.Configs
{
    /// <summary>
    /// Represents configuration settings for connecting to a blob storage service.
    /// </summary>
    public class BlobConfigs
    {
        /// <summary>
        /// Gets or sets the connection string used to access the blob storage.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Contains the name of the blob container where files will be stored.
        /// </summary>
        public string ContainerName { get; set; }
    }
}
