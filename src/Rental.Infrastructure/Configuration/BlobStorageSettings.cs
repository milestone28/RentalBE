

namespace Rental.Infrastructure.Configuration
{
    internal class BlobStorageSettings
    {
        public string ConnectionString { get; set; } = default!;
        public string ContainerName { get; set; } = default!;
        public string AccountName { get; set; } = default!;
        public string AccountKey { get; set; } = default!;
    }
}
