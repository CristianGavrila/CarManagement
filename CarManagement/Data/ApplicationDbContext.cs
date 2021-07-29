using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CarManagement.Models
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BodyStyle> BodyStyles { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<CarFuel> CarFuels { get; set; }
        public virtual DbSet<CarLocation> CarLocations { get; set; }
        public virtual DbSet<Detail> Details { get; set; }
        public virtual DbSet<Manufacturer> Manufacturers { get; set; }
        public virtual DbSet<Model> Models { get; set; }
        public virtual DbSet<ModelYear> ModelYears { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("data source=.\\SQLEXPRESS;Database=Cars;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<BodyStyle>(entity =>
            {
                entity.HasKey(e => e.IdBody)
                    .HasName("PK_BodyStyle");

                entity.Property(e => e.Body)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasKey(e => e.IdCar)
                    .HasName("PK_Cars_CarID");

                entity.HasOne(d => d.Body)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.BodyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cars_Body_BodyId");

                entity.HasOne(d => d.Detail)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.DetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Fuel)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.FuelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cars_Fuels_FuelId");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cars_Locations_LocationId");

                entity.HasOne(d => d.Model)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.ModelId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Year)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.YearId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cars_Years_YearId");
            });

            modelBuilder.Entity<CarFuel>(entity =>
            {
                entity.HasKey(e => e.IdFuel)
                    .HasName("PK_FUEL");

                entity.Property(e => e.Fuel)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CarLocation>(entity =>
            {
                entity.HasKey(e => e.IdLocation)
                    .HasName("PK_Location");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Detail>(entity =>
            {
                entity.HasKey(e => e.IdDetail)
                    .HasName("PK_Details_DetailId");

                entity.Property(e => e.EngineCapacity).HasColumnType("decimal(2, 1)");
            });

            modelBuilder.Entity<Manufacturer>(entity =>
            {
                entity.HasKey(e => e.IdManufacturer)
                    .HasName("PK_Manufacturer");

                entity.Property(e => e.ManufacturerName)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Model>(entity =>
            {
                entity.HasKey(e => e.IdModel)
                    .HasName("PK_Model");

                entity.Property(e => e.ModelName)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.Manufacturer)
                    .WithMany(p => p.Models)
                    .HasForeignKey(d => d.ManufacturerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Model_Manufacturer_ManufacturerId");
            });

            modelBuilder.Entity<ModelYear>(entity =>
            {
                entity.HasKey(e => e.IdYear)
                    .HasName("PK_Year");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
