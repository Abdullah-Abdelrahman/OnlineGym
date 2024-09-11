using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineGym.Entities.Models;

namespace OnlineGym.DataAccess.Data;

public partial class OnlineGymContext :IdentityDbContext<IdentityUser>
{
    public OnlineGymContext()
    {
    }

    public OnlineGymContext(DbContextOptions<OnlineGymContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ClientSubscription> ClientSubscriptions { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

 

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<EmployeeRate> EmployeesRates { get; set; }

    public virtual DbSet<JobTitle> JobTitles { get; set; }



    public virtual DbSet<Leave> Leaves { get; set; }

    public virtual DbSet<Salary> Salaries { get; set; }

    public virtual DbSet<ClientSubscriptionDetails> ClientSubscriptionDetails { get; set; }

    public virtual DbSet<ClientSubscriptionDetailsEmployee> ClientSubscriptionDetailsEmployees { get; set; }


    public virtual DbSet<Benefit> Benefits { get; set; }    


    public virtual DbSet<SubscriptionBenefit> SubscriptionBenefits { get; set; }

    public virtual DbSet<BenefitJobTitle> BenefitJobTitles { get; set; }


    public virtual DbSet<Video> Videos { get; set; }

    public virtual DbSet<TrainingPlan> TrainingPlans { get; set; }

    public virtual DbSet<Day> Days { get; set; }

    public virtual DbSet<Exercise> Exercise { get; set; }   

    public virtual DbSet<DayExercise> DayExercise { get; set; }

    public virtual DbSet<SalaryHistory> SalaryHistories { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseSqlServer("Data Source=DESKTOP-30J4B23\\SQLEXPRESS;Initial Catalog= OnlineGym ;Integrated Security=True;Connect Timeout=100;Trust Server Certificate=True");
    }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<Client>(entity =>
        {



			entity.HasMany(e => e.ClientSubscriptions)
			  .WithOne(e => e.Client)
			  .HasForeignKey(e => e.ClientId)
			  .OnDelete(DeleteBehavior.Cascade);


		});

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.SubscriptionId).HasName("PK__Subscrip__863A7EC12F00CCAA");

            entity.Property(e => e.SubscriptionId).HasColumnName("subscription_id");
            entity.Property(e => e.DurationDays).HasColumnName("duration_days");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.SubscriptionName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("subscription_name");

			entity.HasMany(e => e.ClientSubscriptions)
		  .WithOne(e => e.Subscription)
		  .HasForeignKey(e => e.SubscriptionId)
		  .OnDelete(DeleteBehavior.Cascade);
		});


		modelBuilder.Entity<ClientSubscription>(entity =>
		{
			entity.ToTable("ClientSubscriptions");
			entity.HasKey(e => e.ClientSubscriptionId).HasName("PK_ClientSubscriptions");

			entity.Property(e => e.ClientId).HasColumnName("client_id");

			entity.Property(e => e.Status)
				.HasMaxLength(20)
				.IsUnicode(false)
				.HasDefaultValue("active")
				.HasColumnName("status");
			entity.Property(e => e.SubscriptionId).HasColumnName("subscription_id");

			entity.HasOne(d => d.Client)
				 .WithMany(p => p.ClientSubscriptions)
				 .HasForeignKey(d => d.ClientId)
				 .HasConstraintName("FK_ClientSubscriptions_Client");

			entity.HasOne(d => d.Subscription)
				  .WithMany(p => p.ClientSubscriptions)
				  .HasForeignKey(d => d.SubscriptionId)
				  .HasConstraintName("FK_ClientSubscriptions_Subscription");
		});


		modelBuilder.Entity<Employee>(entity =>
        {

            entity.Property(e => e.EmployeeId).HasColumnName("Employee_id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone");

            entity.Property(e => e.HireDate).HasDefaultValueSql("GETDATE()");


        });



        modelBuilder.Entity<EmployeeRate>(entity =>
        {





        });



        modelBuilder.Entity<JobTitle>(entity =>
        {

            entity.Property(e => e.JobTitleId).HasColumnName("JobTitle_id");

            entity.Property(e => e.JopName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");


        });


        modelBuilder.Entity<Leave>(entity =>
        {


            entity.Property(e => e.Date).HasDefaultValueSql("GETDATE()");


        });


        modelBuilder.Entity<Salary>(entity =>
        {

            entity.Property(e => e.Amount).HasComputedColumnSql("[MounthSalary]+[bonus]");

            entity.Property(e => e.bonus).HasDefaultValue(0);


        });

		modelBuilder.Entity<Subscription>()
		.HasMany(s => s.Benefits)
		.WithMany(b => b.Subscriptions)
		.UsingEntity<SubscriptionBenefit>(
			j => j
				.HasOne(sb => sb.Benefit)
				.WithMany(b => b.SubscriptionBenefits)
				.HasForeignKey(sb => sb.BenefitId),
			j => j
				.HasOne(sb => sb.Subscription)
				.WithMany(s => s.SubscriptionBenefits)
				.HasForeignKey(sb => sb.SubscriptionId),
			j =>
			{
				j.HasKey(t => new { t.BenefitId, t.SubscriptionId });
			}
		);


       
           modelBuilder.Entity<Benefit>()
       .HasMany(s => s.jobTitles)
       .WithMany(b => b.benefits)
       .UsingEntity<BenefitJobTitle>(
        j => j
            .HasOne(sb => sb.JobTitle)
            .WithMany(b => b.benefitJobs)
            .HasForeignKey(sb => sb.JobTitleId),
        j => j
            .HasOne(sb => sb.Benefit)
            .WithMany(s => s.benefitJobTitles)
            .HasForeignKey(sb => sb.BenefitId),
        j =>
        {
            j.HasKey(t => new { t.BenefitId, t.JobTitleId });
        }
    );


		modelBuilder.Entity<Employee>()
            .HasMany(s => s.ClientSubscriptionDetails)
            .WithMany(b => b.ClientSelectedTeam)
            .UsingEntity<ClientSubscriptionDetailsEmployee>(
             j => j
            	 .HasOne(sb => sb.ClientSubscriptionDetails)
            	 .WithMany(b => b.ClientSelectedEmployees)
            	 .HasForeignKey(sb => sb.ClientSubscriptionId),
             j => j
            	 .HasOne(sb => sb.Employee)
            	 .WithMany(s => s.clientSubscriptionDetailsEmployees)
            	 .HasForeignKey(sb => sb.EmployeeId),
             j =>
             {
            	 j.HasKey(t => new { t.EmployeeId, t.ClientSubscriptionId });
             }
            );


        modelBuilder.Entity<Day>()
        .HasMany(s => s.exercises)
        .WithMany(b => b.days)
        .UsingEntity<DayExercise>(
         j => j
             .HasOne(sb => sb.Exercise)
             .WithMany(b => b.dayExercises)
             .HasForeignKey(sb => sb.ExerciseId),
         j => j
             .HasOne(sb => sb.day)
             .WithMany(s => s.dayExercises)
             .HasForeignKey(sb => sb.dayId),
         j =>
         {
             j.HasKey(t => new { t.dayId, t.ExerciseId });
         }
        );

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
