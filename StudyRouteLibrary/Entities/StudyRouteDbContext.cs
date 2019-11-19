using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace StudyRouteLibrary.Entities
{
    public partial class StudyRouteDbContext : DbContext
    {
        /*public StudyRouteDbContext()
        {
        }*/

        public StudyRouteDbContext(DbContextOptions<StudyRouteDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Colleges> Colleges { get; set; }
        public virtual DbSet<Programs> Programs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=project.cyud5gxyu1tc.us-east-2.rds.amazonaws.com,1433; Database= StudyRouteDb;User ID=admin;Password=7rMdO8kTlCM4BDXpI8Gc;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Colleges>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Ratings)
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Programs>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AdmissionRequirements)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.CampusLocation)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CampusName)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Coop)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.ProgramType)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.SummerBreak)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.College)
                    .WithMany(p => p.Programs)
                    .HasForeignKey(d => d.CollegeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Programs__Colleg__412EB0B6");
            });
        }
    }
}
