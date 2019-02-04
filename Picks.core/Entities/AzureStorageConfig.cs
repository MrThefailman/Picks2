
namespace Picks.core.Entities
{
    public class AzureStorageConfig
    {
        public string AccountName { get; set; }
        public string AccountKey { get; set; }
        public string Connectionstring { get; set; }
        public string FullSizeContainer { get; set; }
        public string ThumbnailsContainer { get; set; }

        public string BlobBaseUrl { get; set; }
    }
}
