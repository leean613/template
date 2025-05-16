using Common.Helpers;
using Entities.Buffalo;
using Entities.ReactConfigurations;
using EntityFrameworkCore.Audits;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Contexts
{
    public class AlcareDbContext : DbContext
    {
        private readonly IAuditHelper _auditHelper;
        public AlcareDbContext(DbContextOptions<AlcareDbContext> options, IAuditHelper auditHelper)
            : base(options)
        {
            _auditHelper = auditHelper;
        }

        public AlcareDbContext(DbContextOptions<AlcareDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("connectionconfig.json", false, true)
                .Build();
            var connectionString = configuration.GetConnectionStringByEnv();
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);

        }

        public override int SaveChanges()
        {
            try
            {
                ApplyConcepts();
                var result = base.SaveChanges();

                return result;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DBConcurrencyException(ex.Message, ex);
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                ApplyConcepts();
                var result = await base.SaveChangesAsync(cancellationToken);
                return result;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DBConcurrencyException(ex.Message, ex);
            }
        }

        protected virtual void ApplyConcepts()
        {
            foreach (var entry in ChangeTracker.Entries().ToList())
            {
                _auditHelper.ApplyConcepts(entry);
            }
        }

        public DbSet<User> Users { get; set; }
    }
}
