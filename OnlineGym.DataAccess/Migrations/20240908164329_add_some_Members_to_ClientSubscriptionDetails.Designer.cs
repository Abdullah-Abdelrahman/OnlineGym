﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineGym.DataAccess.Data;

#nullable disable

namespace OnlineGym.DataAccess.Migrations
{
    [DbContext(typeof(OnlineGymContext))]
    [Migration("20240908164329_add_some_Members_to_ClientSubscriptionDetails")]
    partial class add_some_Members_to_ClientSubscriptionDetails
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ClientSubscription", b =>
                {
                    b.Property<string>("ClientsId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("SubscriptionId")
                        .HasColumnType("int");

                    b.HasKey("ClientsId", "SubscriptionId");

                    b.HasIndex("SubscriptionId");

                    b.ToTable("ClientSubscription");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("OnlineGym.Entities.Models.Benefit", b =>
                {
                    b.Property<int>("BenefitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BenefitId"));

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BenefitId");

                    b.ToTable("Benefits");
                });

            modelBuilder.Entity("OnlineGym.Entities.Models.BenefitJobTitle", b =>
                {
                    b.Property<int>("BenefitId")
                        .HasColumnType("int");

                    b.Property<int>("JobTitleId")
                        .HasColumnType("int");

                    b.HasKey("BenefitId", "JobTitleId");

                    b.HasIndex("JobTitleId");

                    b.ToTable("BenefitJobTitles");
                });

            modelBuilder.Entity("OnlineGym.Entities.Models.ClientSubscription", b =>
                {
                    b.Property<int>("ClientSubscriptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClientSubscriptionId"));

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("client_id");

                    b.Property<string>("PaymentIntentId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SessionId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasDefaultValue("active")
                        .HasColumnName("status");

                    b.Property<int>("SubscriptionId")
                        .HasColumnType("int")
                        .HasColumnName("subscription_id");

                    b.Property<DateTime?>("orderDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("price")
                        .HasColumnType("int");

                    b.HasKey("ClientSubscriptionId")
                        .HasName("PK_ClientSubscriptions");

                    b.HasIndex("ClientId");

                    b.HasIndex("SubscriptionId");

                    b.ToTable("ClientSubscriptions", (string)null);
                });

            modelBuilder.Entity("OnlineGym.Entities.Models.ClientSubscriptionDetails", b =>
                {
                    b.Property<int>("ClientSubscriptionId")
                        .HasColumnType("int");

                    b.Property<int?>("Age")
                        .HasColumnType("int");

                    b.Property<float?>("BodyFat")
                        .HasColumnType("real");

                    b.Property<string>("ClientEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClientPhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Diseases")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly?>("EndDate")
                        .HasColumnType("date");

                    b.Property<bool?>("Gender")
                        .HasColumnType("bit");

                    b.Property<int?>("Hight")
                        .HasColumnType("int");

                    b.Property<DateOnly?>("StartDate")
                        .HasColumnType("date");

                    b.Property<string>("Target")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float?>("Weight")
                        .HasColumnType("real");

                    b.Property<DateTime?>("paymentDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ClientSubscriptionId");

                    b.ToTable("ClientSubscriptionDetails");
                });

            modelBuilder.Entity("OnlineGym.Entities.Models.ClientSubscriptionDetailsEmployee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("ClientSubscriptionId")
                        .HasColumnType("int");

                    b.HasKey("EmployeeId", "ClientSubscriptionId");

                    b.HasIndex("ClientSubscriptionId");

                    b.ToTable("ClientSubscriptionDetailsEmployees");
                });

            modelBuilder.Entity("OnlineGym.Entities.Models.Day", b =>
                {
                    b.Property<int>("DayId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DayId"));

                    b.Property<int>("TrainingPlanId")
                        .HasColumnType("int");

                    b.Property<bool>("isDone")
                        .HasColumnType("bit");

                    b.HasKey("DayId");

                    b.HasIndex("TrainingPlanId");

                    b.ToTable("Days");
                });

            modelBuilder.Entity("OnlineGym.Entities.Models.DayExercise", b =>
                {
                    b.Property<int>("dayId")
                        .HasColumnType("int");

                    b.Property<int>("ExerciseId")
                        .HasColumnType("int");

                    b.Property<bool>("isDone")
                        .HasColumnType("bit");

                    b.HasKey("dayId", "ExerciseId");

                    b.HasIndex("ExerciseId");

                    b.ToTable("DayExercise");
                });

            modelBuilder.Entity("OnlineGym.Entities.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Employee_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("email");

                    b.Property<DateTime>("HireDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("JobTitleId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name");

                    b.Property<string>("Phone")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("phone");

                    b.Property<bool>("gender")
                        .HasColumnType("bit");

                    b.HasKey("EmployeeId");

                    b.HasIndex("JobTitleId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("OnlineGym.Entities.Models.EmployeeRate", b =>
                {
                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("Rate")
                        .HasColumnType("int");

                    b.HasKey("EmployeeId");

                    b.ToTable("EmployeesRates");
                });

            modelBuilder.Entity("OnlineGym.Entities.Models.Exercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VideoUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Exercise");
                });

            modelBuilder.Entity("OnlineGym.Entities.Models.JobTitle", b =>
                {
                    b.Property<int>("JobTitleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("JobTitle_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("JobTitleId"));

                    b.Property<string>("JopDiscription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JopName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name");

                    b.Property<int>("MaxSalary")
                        .HasColumnType("int");

                    b.Property<int>("MinSalary")
                        .HasColumnType("int");

                    b.HasKey("JobTitleId");

                    b.ToTable("JobTitles");
                });

            modelBuilder.Entity("OnlineGym.Entities.Models.Leave", b =>
                {
                    b.Property<int>("LeaveId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LeaveId"));

                    b.Property<DateTime?>("Date")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("reason")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LeaveId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Leaves");
                });

            modelBuilder.Entity("OnlineGym.Entities.Models.Salary", b =>
                {
                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("Amount")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("int")
                        .HasComputedColumnSql("[MounthSalary]+[bonus]");

                    b.Property<int>("MounthSalary")
                        .HasColumnType("int");

                    b.Property<int>("bonus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<DateTime>("nextSalaryDate")
                        .HasColumnType("datetime2");

                    b.HasKey("EmployeeId");

                    b.ToTable("Salaries");
                });

            modelBuilder.Entity("OnlineGym.Entities.Models.SalaryHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<bool>("Paid")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("PaidAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("bonus")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("SalaryHistories");
                });

            modelBuilder.Entity("OnlineGym.Entities.Models.Subscription", b =>
                {
                    b.Property<int>("SubscriptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("subscription_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubscriptionId"));

                    b.Property<int>("DurationDays")
                        .HasColumnType("int")
                        .HasColumnName("duration_days");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10, 2)")
                        .HasColumnName("price");

                    b.Property<string>("SubscriptionName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("subscription_name");

                    b.HasKey("SubscriptionId")
                        .HasName("PK__Subscrip__863A7EC12F00CCAA");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("OnlineGym.Entities.Models.SubscriptionBenefit", b =>
                {
                    b.Property<int>("BenefitId")
                        .HasColumnType("int");

                    b.Property<int>("SubscriptionId")
                        .HasColumnType("int");

                    b.HasKey("BenefitId", "SubscriptionId");

                    b.HasIndex("SubscriptionId");

                    b.ToTable("SubscriptionBenefits");
                });

            modelBuilder.Entity("OnlineGym.Entities.Models.TrainingPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("TrainingPlans");
                });

            modelBuilder.Entity("OnlineGym.Entities.Models.Video", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("category")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Videos");
                });

            modelBuilder.Entity("OnlineGym.Entities.Models.Client", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Client");
                });

            modelBuilder.Entity("ClientSubscription", b =>
                {
                    b.HasOne("OnlineGym.Entities.Models.Client", null)
                        .WithMany()
                        .HasForeignKey("ClientsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineGym.Entities.Models.Subscription", null)
                        .WithMany()
                        .HasForeignKey("SubscriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OnlineGym.Entities.Models.BenefitJobTitle", b =>
                {
                    b.HasOne("OnlineGym.Entities.Models.Benefit", "Benefit")
                        .WithMany("benefitJobTitles")
                        .HasForeignKey("BenefitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineGym.Entities.Models.JobTitle", "JobTitle")
                        .WithMany("benefitJobs")
                        .HasForeignKey("JobTitleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Benefit");

                    b.Navigation("JobTitle");
                });

            modelBuilder.Entity("OnlineGym.Entities.Models.ClientSubscription", b =>
                {
                    b.HasOne("OnlineGym.Entities.Models.Client", "Client")
                        .WithMany("ClientSubscriptions")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_ClientSubscriptions_Client");

                    b.HasOne("OnlineGym.Entities.Models.Subscription", "Subscription")
                        .WithMany("ClientSubscriptions")
                        .HasForeignKey("SubscriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_ClientSubscriptions_Subscription");

                    b.Navigation("Client");

                    b.Navigation("Subscription");
                });

            modelBuilder.Entity("OnlineGym.Entities.Models.ClientSubscriptionDetails", b =>
                {
                    b.HasOne("OnlineGym.Entities.Models.ClientSubscription", "clientSubscription")
                        .WithOne("ClientSubscriptionDetails")
                        .HasForeignKey("OnlineGym.Entities.Models.ClientSubscriptionDetails", "ClientSubscriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("clientSubscription");
                });

            modelBuilder.Entity("OnlineGym.Entities.Models.ClientSubscriptionDetailsEmployee", b =>
                {
                    b.HasOne("OnlineGym.Entities.Models.ClientSubscriptionDetails", "ClientSubscriptionDetails")
                        .WithMany("ClientSelectedEmployees")
                        .HasForeignKey("ClientSubscriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineGym.Entities.Models.Employee", "Employee")
                        .WithMany("clientSubscriptionDetailsEmployees")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClientSubscriptionDetails");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("OnlineGym.Entities.Models.Day", b =>
                {
                    b.HasOne("OnlineGym.Entities.Models.TrainingPlan", "TrainingPlan")
                        .WithMany("Days")
                        .HasForeignKey("TrainingPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TrainingPlan");
                });

            modelBuilder.Entity("OnlineGym.Entities.Models.DayExercise", b =>
                {
                    b.HasOne("OnlineGym.Entities.Models.Exercise", "Exercise")
                        .WithMany("dayExercises")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineGym.Entities.Models.Day", "day")
                        .WithMany("dayExercises")
                        .HasForeignKey("dayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exercise");

                    b.Navigation("day");
                });

            modelBuilder.Entity("OnlineGym.Entities.Models.Employee", b =>
                {
                    b.HasOne("OnlineGym.Entities.Models.JobTitle", "JobTitle")
                        .WithMany("Employees")
                        .HasForeignKey("JobTitleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("JobTitle");
                });

            modelBuilder.Entity("OnlineGym.Entities.Models.EmployeeRate", b =>
                {
                    b.HasOne("OnlineGym.Entities.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("OnlineGym.Entities.Models.Leave", b =>
                {
                    b.HasOne("OnlineGym.Entities.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("OnlineGym.Entities.Models.Salary", b =>
                {
                    b.HasOne("OnlineGym.Entities.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("OnlineGym.Entities.Models.SalaryHistory", b =>
                {
                    b.HasOne("OnlineGym.Entities.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("OnlineGym.Entities.Models.SubscriptionBenefit", b =>
                {
                    b.HasOne("OnlineGym.Entities.Models.Benefit", "Benefit")
                        .WithMany("SubscriptionBenefits")
                        .HasForeignKey("BenefitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineGym.Entities.Models.Subscription", "Subscription")
                        .WithMany("SubscriptionBenefits")
                        .HasForeignKey("SubscriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Benefit");

                    b.Navigation("Subscription");
                });

            modelBuilder.Entity("OnlineGym.Entities.Models.TrainingPlan", b =>
                {
                    b.HasOne("OnlineGym.Entities.Models.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("OnlineGym.Entities.Models.Benefit", b =>
                {
                    b.Navigation("SubscriptionBenefits");

                    b.Navigation("benefitJobTitles");
                });

            modelBuilder.Entity("OnlineGym.Entities.Models.ClientSubscription", b =>
                {
                    b.Navigation("ClientSubscriptionDetails");
                });

            modelBuilder.Entity("OnlineGym.Entities.Models.ClientSubscriptionDetails", b =>
                {
                    b.Navigation("ClientSelectedEmployees");
                });

            modelBuilder.Entity("OnlineGym.Entities.Models.Day", b =>
                {
                    b.Navigation("dayExercises");
                });

            modelBuilder.Entity("OnlineGym.Entities.Models.Employee", b =>
                {
                    b.Navigation("clientSubscriptionDetailsEmployees");
                });

            modelBuilder.Entity("OnlineGym.Entities.Models.Exercise", b =>
                {
                    b.Navigation("dayExercises");
                });

            modelBuilder.Entity("OnlineGym.Entities.Models.JobTitle", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("benefitJobs");
                });

            modelBuilder.Entity("OnlineGym.Entities.Models.Subscription", b =>
                {
                    b.Navigation("ClientSubscriptions");

                    b.Navigation("SubscriptionBenefits");
                });

            modelBuilder.Entity("OnlineGym.Entities.Models.TrainingPlan", b =>
                {
                    b.Navigation("Days");
                });

            modelBuilder.Entity("OnlineGym.Entities.Models.Client", b =>
                {
                    b.Navigation("ClientSubscriptions");
                });
#pragma warning restore 612, 618
        }
    }
}
