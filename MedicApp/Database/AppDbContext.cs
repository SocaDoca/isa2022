using MedicApp.Models;
using MedicApp.RelationshipTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MySql.EntityFrameworkCore.Extensions;

namespace MedicApp.Database
{
    public class AppDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public AppDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to mysql with connection string from app settings
            var connectionString = Configuration.GetConnectionString("MySQLConnection");
            options.UseMySQL(connectionString);
        }

        #region DbSets 

        public DbSet<Clinic>Clinics { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Laboratory> Laboratories{ get; set; }
        public DbSet<CommunicationChannel> CommunicationChannels{ get; set; }
        public DbSet<Clinic2Laboratory> Clinic2Laboratories { get; set; }
        public DbSet<Clinic2CommunicationChannels> Clinic2CommunicationChannels { get; set; }
        //public DbSet<Clinic> Clinics { get; set; }
        //public DbSet<Clinic> Clinics { get; set; }
        //public DbSet<Clinic> Clinics { get; set; }
        #endregion
    }

    public class MysqlEntityFrameworkDesignTimeServices : IDesignTimeServices
    {
        public void ConfigureDesignTimeServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddEntityFrameworkMySQL();
            new EntityFrameworkRelationalDesignServicesBuilder(serviceCollection)
                .TryAddCoreServices();
        }
    }

}
