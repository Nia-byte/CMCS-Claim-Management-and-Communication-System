using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Identity;
using POEFINAL_CMCS_ST10396650.Pages;

namespace POEFINAL_CMCS_ST10396650
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
               : base(options)
        {
        }
        public DbSet<Lecture> Lecturers { get; set; }
        public DbSet<POEFINAL_CMCS_ST10396650.ClaimModel> Claims { get; set; }

       public DbSet<ProgrammeCoordinator> ProgrammeCoordinator { get; set; }

       public DbSet<AcademicManager> AcademicManager { get; set; }
        public DbSet<POEFINAL_CMCS_ST10396650.ReviewedClaim> ReviewedClaims { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         //   modelBuilder.Entity<ProgrammeCoordinator>()
          //      .HasKey(pc => pc.CoordinatorId); // Define the primary key
            modelBuilder.Entity<Lecture>()
            .HasKey(l => l.lecturerId);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<POEFINAL_CMCS_ST10396650.ClaimModel>(entity =>
            {
                entity.HasKey(e => e.ClaimId);
                entity.Property(e => e.Total).HasColumnType("decimal(18,2)");
                entity.Property(e => e.HoursWorked).HasColumnType("decimal(18,2)");
                entity.Property(e => e.HourlyRate).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<POEFINAL_CMCS_ST10396650.ClaimModel>().ToTable("Claims");


            modelBuilder.Entity<ReviewedClaim>(entity =>
            {
                entity.ToTable("ReviewedClaims");

                entity.HasKey(e => e.ReviewId);

                entity.Property(e => e.ReviewId)
                    .UseIdentityColumn();

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.StatusApproval)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ReviewedDate)
                    .HasDefaultValueSql("GETDATE()");

                entity.HasOne(d => d.Claim)
                    .WithMany()
                    .HasForeignKey(d => d.ClaimId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(d => d.Lecturer)
                    .WithMany()
                    .HasForeignKey(d => d.LecturerId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(d => d.Coordinator)
                    .WithMany()
                    .HasForeignKey(d => d.CoordinatorId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Notification>()
            .HasOne(n => n.ReviewedClaim)
            .WithMany()
            .HasForeignKey(n => n.ReviewedClaimId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // Additional configurations...
    }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=labVMH8OX\\SQLEXPRESS;Initial Catalog=CMCSDB;Integrated Security=True;Trust Server Certificate=True"); // Use your database connection string
        }
    }
} //(Medium, 2023)
