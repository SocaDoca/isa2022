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

        public DbSet<DbClinic>Clinics { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<DbCommunicationChannel> CommunicationChannels{ get; set; }

        public DbSet<DbEmployee> Employees { get; set; }
        public DbSet<DbPersonInfo> PersonInfos{ get; set; }
        public DbSet<DbSessions> Sessions{ get; set; }

        #region AssignmentTables
        public DbSet<Clinic2Laboratory> Clinic2Laboratories { get; set; }
        public DbSet<Clinic2CommunicationChannels> Clinic2CommunicationChannels { get; set; }
        #endregion
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
