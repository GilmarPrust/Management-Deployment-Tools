
namespace DCM.API.Helpers
{
    public class ConnectionProviderConfiguration(IConfiguration configuration) : IConnectionProvider
    {
        private readonly IConfiguration _configuration = configuration;

        public string GetConnectionString(string name = "DefaultConnection")
        {
            var conn = _configuration.GetConnectionString(name);
            if (string.IsNullOrWhiteSpace(conn))
                throw new InvalidOperationException($"Connection string '{name}' não encontrada no appsettings.json.");
            return conn;
        }
    }
}