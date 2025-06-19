using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DataAccessObjects
{
    public class MyStoreContextFactory : IDesignTimeDbContextFactory<MyStoreContext>
    {
        public MyStoreContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyStoreContext>();
            
            // Đọc connection string từ appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnectionString");
            
            if (string.IsNullOrEmpty(connectionString))
            {
                // Fallback connection string nếu không tìm thấy trong appsettings
                connectionString = "Server=.;Database=ProductManagementDB;Trusted_Connection=True;TrustServerCertificate=True;";
            }

            optionsBuilder.UseSqlServer(connectionString);

            return new MyStoreContext(optionsBuilder.Options);
        }
    }
} 