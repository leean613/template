using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace EntityFrameworkCore.Contexts
{
    public class AlcareDbContextFactory : IDesignTimeDbContextFactory<AlcareDbContext>
    {
        public AlcareDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("connectionconfig.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<AlcareDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Development"));

            return new AlcareDbContext(optionsBuilder.Options);
        }
    }
}
