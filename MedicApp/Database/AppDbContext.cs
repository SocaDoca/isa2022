using MedicApp.Enums;
using MedicApp.Integrations;
using MedicApp.Models;
using MedicApp.RelationshipTables;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MySql.EntityFrameworkCore.Extensions;
using System.Configuration;
using System.Drawing;

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

        public DbSet<User> Users { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Grades> Grades { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentReport> AppointmentsReports { get; set; }
        public DbSet<Questionnaire> Questionnaire { get; set; }
        public DbSet<AppointmentHistory> AppointmentHistories { get; set; }
        public DbSet<WorkItem> WorkItems { get; set; }
        public DbSet<WorkItem2Reports> WorkItem2Reports { get; set; }
        public DbSet<Complaints> Complaints { get; set; }


        #region Assignment Tables
        public DbSet<Clinic2WorkingHours> Clinic2WorkingHours { get; set; }
        public DbSet<Clinic2Address> Clinic2Addresses { get; set; }
        public DbSet<Clinic2Employee> Clinic2Employees { get; set; }
        public DbSet<ClinicRating2Patient> ClinicRating2Patients { get; set; }


        public DbSet<Appointment2Report> Appointment2Reports { get; set; }
        public DbSet<Appointment2Patient> Appointment2Patients { get; set; }
        public DbSet<Appointment2Doctor> Appointment2Doctors { get; set; }
        public DbSet<Appointment2Clinic> Appointment2Clinics { get; set; }

        public DbSet<Account2Clinic> Account2Clinics { get; set; }
        public DbSet<Account2Patient> Account2Patients { get; set; }
        public DbSet<Employee2WokringHours> Employee2WokringHours{ get; set; }
        public DbSet<Complaint2Patient> Complaint2Patients { get; set; }


        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
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


}
