﻿// <auto-generated />
using System;
using MedicApp.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MedicApp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230125215524_firsttime")]
    partial class firsttime
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<Guid?>("Patient_RefID")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("ResponsiblePerson_RefID")
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

                    b.Property<int>("AppointmentStatus")
                        .HasColumnType("int");

                    b.Property<Guid>("ChangedByUser_RefID")
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("TimeCreated")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("AppointmentHistories");
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

                    b.Property<float>("Rating")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Clinics");
                });

            modelBuilder.Entity("MedicApp.Models.Questionnaire", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("ExpireDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsValid")
                        .HasColumnType("tinyint(1)");

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

            modelBuilder.Entity("MedicApp.Models.WorkingHours", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("DayOfWeek")
                        .HasColumnType("int");

                    b.Property<string>("End")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Start")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("WorkingHours");
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

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("WorkingHours_RefID")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("Clinic2WorkingHours");
                });

            modelBuilder.Entity("MedicApp.RelationshipTables.Employee2WokringHours", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("Employee_RefID")
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("WorkingHours_RefID")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("Employee2WokringHours");
                });

            modelBuilder.Entity("MedicApp.RelationshipTables.Patient2Questionnaire", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("Patient_RefId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("Questionnaire_RefId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("Patient2Questionnaires");
                });
#pragma warning restore 612, 618
        }
    }
}