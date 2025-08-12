namespace DCM.API.Helpers
{
    public static class DbContextRegistration
    {
        public static IServiceCollection AddAppDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            // Registra o provider de conex�o
            services.AddSingleton<IConnectionProvider, ConnectionProviderConfiguration>();

            // Define o banco de dados a ser usado via configura��o
            var dbType = configuration.GetValue<string>("Database:Provider")?.ToLower() ?? "sqlite";

            services.AddDbContext<AppDbContext>((serviceProvider, options) =>
            {
                var connectionProvider = serviceProvider.GetRequiredService<IConnectionProvider>();
                var connectionString = connectionProvider.GetConnectionString();

                if (dbType == "sqlserver")
                {
                    options.UseSqlServer(
                        connectionString,
                        sqlOptions => sqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
                    );
                }
                else // padr�o: sqlite
                {
                    options.UseSqlite(
                        connectionString,
                        sqliteOptions => sqliteOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
                    );
                }
            });

            return services;
        }
    }
}
