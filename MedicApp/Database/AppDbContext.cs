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

        public DbSet<DbClinic> Clinics { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<DbAddress> Addresses { get; set; }
        public DbSet<DbWorkingHours> WorkingHours { get; set; }
        public DbSet<DbSessions> Sessions { get; set; }
        public DbSet<DbPatient> Patients { get; set; }
        public DbSet<DbAppointment> Appointments { get; set; }
        public DbSet<DbEmployee> Employees { get; set; }

        #region AssignmentTables
        public DbSet<Clinic2Address> Clinic2Addresses { get; set; }
        public DbSet<Clinic2WorkingHours> Clinic2WorkingHours { get; set; }
        public DbSet<Clinic2AdminCenter> Clinic2AdminCenters { get; set; }
        public DbSet<Clinic2Employee> Clinic2Employees { get; set; }
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
