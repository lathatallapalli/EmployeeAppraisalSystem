﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Plugins.DataStore.SQLServer;

#nullable disable

namespace Plugins.DataStore.SQLServer.Migrations
{
    [DbContext(typeof(AppraisalSystemContext))]
    [Migration("20240325104359_InitTestDemo")]
    partial class InitTestDemo
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CoreBusiness.Appraisal", b =>
                {
                    b.Property<int>("AppraisalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AppraisalId"));

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int?>("EmployeeId1")
                        .HasColumnType("int");

                    b.Property<int?>("EmployeeId2")
                        .HasColumnType("int");

                    b.Property<int>("ManagerId")
                        .HasColumnType("int");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.HasKey("AppraisalId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("EmployeeId1");

                    b.HasIndex("EmployeeId2");

                    b.ToTable("Appraisals");
                });

            modelBuilder.Entity("CoreBusiness.AppraisalDetailsCompetency", b =>
                {
                    b.Property<int>("DetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DetailId"));

                    b.Property<int>("AppraisalId")
                        .HasColumnType("int");

                    b.Property<string>("Competency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeeFeedback")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeRating")
                        .HasColumnType("int");

                    b.Property<string>("ManagerFeedback")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ManagerId")
                        .HasColumnType("int");

                    b.Property<int>("ManagerRating")
                        .HasColumnType("int");

                    b.HasKey("DetailId");

                    b.HasIndex("AppraisalId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("ManagerId");

                    b.ToTable("AppraisalDetailsCompetencies");
                });

            modelBuilder.Entity("CoreBusiness.AppraisalDetailsObjective", b =>
                {
                    b.Property<int>("DetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DetailId"));

                    b.Property<int>("AppraisalId")
                        .HasColumnType("int");

                    b.Property<string>("EmployeeFeedback")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeRating")
                        .HasColumnType("int");

                    b.Property<string>("ManagerFeedback")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ManagerId")
                        .HasColumnType("int");

                    b.Property<int>("ManagerRating")
                        .HasColumnType("int");

                    b.Property<string>("Objective")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DetailId");

                    b.HasIndex("AppraisalId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("ManagerId");

                    b.ToTable("AppraisalDetailsObjectives");
                });

            modelBuilder.Entity("CoreBusiness.Competency", b =>
                {
                    b.Property<int>("CompetencyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CompetencyId"));

                    b.Property<string>("CompetencyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("competencyType")
                        .HasColumnType("int");

                    b.HasKey("CompetencyId");

                    b.ToTable("Competencies");

                    b.HasData(
                        new
                        {
                            CompetencyId = 1,
                            CompetencyName = "CyberSecurity",
                            competencyType = 0
                        },
                        new
                        {
                            CompetencyId = 2,
                            CompetencyName = "CloudComputing",
                            competencyType = 0
                        });
                });

            modelBuilder.Entity("CoreBusiness.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"));

                    b.Property<int?>("AdminPermission")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeeDesignation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ManagerId")
                        .HasColumnType("int");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmployeeId");

                    b.HasIndex("ManagerId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            EmployeeId = 1,
                            AdminPermission = 2,
                            Email = "jacob@gmail.com",
                            EmployeeDesignation = "Sr.Dev",
                            EmployeeName = "Jacob",
                            Mobile = "654321",
                            Password = "jacob"
                        },
                        new
                        {
                            EmployeeId = 10,
                            AdminPermission = 3,
                            Email = "john@gmail.com",
                            EmployeeDesignation = "Dev",
                            EmployeeName = "John",
                            ManagerId = 1,
                            Mobile = "321456",
                            Password = "jacob"
                        });
                });

            modelBuilder.Entity("CoreBusiness.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<string>("RoleDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            RoleId = 1,
                            RoleDescription = "",
                            RoleName = "Sr.dev"
                        },
                        new
                        {
                            RoleId = 2,
                            RoleDescription = "",
                            RoleName = "Dev"
                        });
                });

            modelBuilder.Entity("CoreBusiness.RoleCompetencyDetails", b =>
                {
                    b.Property<int>("DetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DetailId"));

                    b.Property<int>("CompetencyId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("DetailId");

                    b.HasIndex("CompetencyId");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleCompetencyDetails");

                    b.HasData(
                        new
                        {
                            DetailId = 1,
                            CompetencyId = 1,
                            RoleId = 1
                        },
                        new
                        {
                            DetailId = 2,
                            CompetencyId = 1,
                            RoleId = 2
                        },
                        new
                        {
                            DetailId = 3,
                            CompetencyId = 2,
                            RoleId = 1
                        },
                        new
                        {
                            DetailId = 4,
                            CompetencyId = 2,
                            RoleId = 2
                        });
                });

            modelBuilder.Entity("CoreBusiness.Appraisal", b =>
                {
                    b.HasOne("CoreBusiness.Employee", "Employee")
                        .WithMany("EmployeeAppraisals")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CoreBusiness.Employee", null)
                        .WithMany("Appraisals")
                        .HasForeignKey("EmployeeId1");

                    b.HasOne("CoreBusiness.Employee", null)
                        .WithMany("ManagerAppraisals")
                        .HasForeignKey("EmployeeId2");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("CoreBusiness.AppraisalDetailsCompetency", b =>
                {
                    b.HasOne("CoreBusiness.Appraisal", "Appraisal")
                        .WithMany("AppraisalDetailsCompetencies")
                        .HasForeignKey("AppraisalId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CoreBusiness.Employee", "Employee")
                        .WithMany("AppraisalDetailsCompetencies")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CoreBusiness.Employee", "Manager")
                        .WithMany()
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Appraisal");

                    b.Navigation("Employee");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("CoreBusiness.AppraisalDetailsObjective", b =>
                {
                    b.HasOne("CoreBusiness.Appraisal", "Appraisal")
                        .WithMany("AppraisalDetailsObjectives")
                        .HasForeignKey("AppraisalId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CoreBusiness.Employee", "Employee")
                        .WithMany("AppraisalDetailsObjectives")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CoreBusiness.Employee", "Manager")
                        .WithMany()
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Appraisal");

                    b.Navigation("Employee");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("CoreBusiness.Employee", b =>
                {
                    b.HasOne("CoreBusiness.Employee", "Manager")
                        .WithMany("ManagedEmployees")
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("CoreBusiness.RoleCompetencyDetails", b =>
                {
                    b.HasOne("CoreBusiness.Competency", "Competency")
                        .WithMany("RoleCompetencyDetails")
                        .HasForeignKey("CompetencyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CoreBusiness.Role", "Role")
                        .WithMany("RoleCompetencyDetails")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Competency");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("CoreBusiness.Appraisal", b =>
                {
                    b.Navigation("AppraisalDetailsCompetencies");

                    b.Navigation("AppraisalDetailsObjectives");
                });

            modelBuilder.Entity("CoreBusiness.Competency", b =>
                {
                    b.Navigation("RoleCompetencyDetails");
                });

            modelBuilder.Entity("CoreBusiness.Employee", b =>
                {
                    b.Navigation("AppraisalDetailsCompetencies");

                    b.Navigation("AppraisalDetailsObjectives");

                    b.Navigation("Appraisals");

                    b.Navigation("EmployeeAppraisals");

                    b.Navigation("ManagedEmployees");

                    b.Navigation("ManagerAppraisals");
                });

            modelBuilder.Entity("CoreBusiness.Role", b =>
                {
                    b.Navigation("RoleCompetencyDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
