namespace LibraryApi.DataAccess.Options
{
    public class ConnectionOptions
    {
        public string MasterConnectionString { get; set; } = string.Empty;

        public string ReadOnlyConnectionString { get; set; } = string.Empty;
    }
}