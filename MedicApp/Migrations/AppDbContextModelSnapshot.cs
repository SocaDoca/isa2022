﻿// <auto-generated />
using System;
using MedicApp.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MedicApp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("MedicApp.Models.Appointment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("Clinic_RefID")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Creation_TimeStamp")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<bool>("IsCanceled")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsFinished")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsPredefiend")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsReserved")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsStarted")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid?>("Patient_RefID")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("MedicApp.Models.AppointmentHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("AppointmentId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ChangedByUser_RefID")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Creation_TimeStamp")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsFinishedAppointment")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsStartedAppointment")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("AppointmentHistories");
                });

            modelBuilder.Entity("MedicApp.Models.AppointmentReport", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Creation_TimeStamp")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("AppointmentsReports");
                });

            modelBuilder.Entity("MedicApp.Models.Clinic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Creation_TimeStamp")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("Rating")
                        .HasColumnType("double");

                    b.Property<DateTime>("WorksFrom")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("WorksTo")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Clinics");
                });

            modelBuilder.Entity("MedicApp.Models.Complaints", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("ComplaintBy_User_RefId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Creation_TimeStamp")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsAnswered")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsForClinic")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid?>("IsForClinic_Clinic_RefId")
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsForEmployee")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid?>("IsForEmployee_User_RefId")
                        .HasColumnType("char(36)");

                    b.Property<string>("UserInput")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Complaints");
                });

            modelBuilder.Entity("MedicApp.Models.Grades", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("Clinic_RefId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Creation_TimeStamp")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("Patient_RefId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("MedicApp.Models.Questionnaire", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Creation_TimeStamp")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ExpireDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsValid")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("Patient_RefID")
                        .HasColumnType("char(36)");

                    b.Property<bool>("question1")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("question10")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("question11")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("question12")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("question2")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("question3")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("question4")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("question5")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("question6")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("question7")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("question8")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("question9")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("Questionnaire");
                });

            modelBuilder.Entity("MedicApp.Models.Roles", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Creation_TimeStamp")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("MedicApp.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Address")
                        .HasColumnType("longtext");

                    b.Property<string>("City")
                        .HasColumnType("longtext");

                    b.Property<string>("Country")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Creation_TimeStamp")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<bool>("IsAdminCenter")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsFirstTime")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("JMBG")
                        .HasColumnType("longtext");

                    b.Property<string>("Job")
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext");

                    b.Property<int>("LoyaltyPoints")
                        .HasColumnType("int");

                    b.Property<string>("Mobile")
                        .HasColumnType("longtext");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.Property<int>("Penalty")
                        .HasColumnType("int");

                    b.Property<string>("Role")
                        .HasColumnType("longtext");

                    b.Property<string>("Username")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MedicApp.Models.WorkItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("AppointmentReportId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Creation_TimeStamp")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UsedInstances")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AppointmentReportId");

                    b.ToTable("WorkItems");
                });

            modelBuilder.Entity("MedicApp.RelationshipTables.Account2Clinic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("Account_RefID")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("Clinic_RefID")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Creation_TimeStamp")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("Account2Clinics");
                });

            modelBuilder.Entity("MedicApp.RelationshipTables.Account2Patient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("Account_RefID")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Creation_TimeStamp")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("Patient_RefID")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("Account2Patients");
                });

            modelBuilder.Entity("MedicApp.RelationshipTables.Appointment2Clinic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("Appointment_RefID")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("Clinic_RefID")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Creation_TimeStamp")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("Appointment2Clinics");
                });

            modelBuilder.Entity("MedicApp.RelationshipTables.Appointment2Doctor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("Appointment_RefID")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Creation_TimeStamp")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("Doctor_RefID")
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("Appointment2Doctors");
                });

            modelBuilder.Entity("MedicApp.RelationshipTables.Appointment2Patient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("Appointment_RefID")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Creation_TimeStamp")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("Patient_RefID")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("Appointment2Patients");
                });

            modelBuilder.Entity("MedicApp.RelationshipTables.Appointment2Report", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("Appointment_RefID")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Creation_TimeStamp")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("ReportId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("Appointment2Reports");
                });

            modelBuilder.Entity("MedicApp.RelationshipTables.Clinic2Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("Address_RefID")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("Clinic_RefID")
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("Clinic2Addresses");
                });

            modelBuilder.Entity("MedicApp.RelationshipTables.Clinic2Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("Clinic_RefID")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Creation_TimeStamp")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("Employee_RefID")
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("Clinic2Employees");
                });

            modelBuilder.Entity("MedicApp.RelationshipTables.Clinic2WorkingHours", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("Clinic_RefID")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Creation_TimeStamp")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("WorkingHours_RefID")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("Clinic2WorkingHours");
                });

            modelBuilder.Entity("MedicApp.RelationshipTables.Complaint2Patient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("Complaint_RefId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Creation_TimeStamp")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("Patient_RefId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("Complaint2Patients");
                });

            modelBuilder.Entity("MedicApp.RelationshipTables.Employee2WokringHours", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Creation_TimeStamp")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("Employee_RefID")
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("WorkingHours_RefID")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("Employee2WokringHours");
                });

            modelBuilder.Entity("MedicApp.RelationshipTables.WorkItem2Reports", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Creation_TimeStamp")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("Report_RefID")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("WorkItem_RefID")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("WorkItem2Reports");
                });

            modelBuilder.Entity("MedicApp.Models.WorkItem", b =>
                {
                    b.HasOne("MedicApp.Models.AppointmentReport", null)
                        .WithMany("Equipment")
                        .HasForeignKey("AppointmentReportId");
                });

            modelBuilder.Entity("MedicApp.Models.AppointmentReport", b =>
                {
                    b.Navigation("Equipment");
                });
#pragma warning restore 612, 618
        }
    }
}
