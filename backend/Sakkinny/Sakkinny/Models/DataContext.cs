using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Sakkinny.Models
{
      public class DataContext : IdentityDbContext<ApplicationUser>
      {
            public DbSet<Apartment> Apartments { get; set; } = null!; // Not nullable
            public DbSet<ApartmentImage> ApartmentImage { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
            {
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                  base.OnModelCreating(modelBuilder);

                  modelBuilder.Entity<ApplicationUser>().ToTable("Users");

                  modelBuilder.Entity<Apartment>(entity =>
                  {
                        entity.HasKey(e => e.Id);
                        entity.Property(e => e.Id)
                        .ValueGeneratedOnAdd();

                        entity.Property(e => e.Title)
                        .IsRequired()
                        .HasMaxLength(100);

                        entity.Property(e => e.SubTitle)
                        .HasMaxLength(500);

                        entity.Property(e => e.Location)
                        .IsRequired()
                        .HasMaxLength(150);

                        entity.Property(e => e.RoomsNumber)
                        .IsRequired(false);

                        entity.Property(e => e.RoomsAvailable)
                        .IsRequired();

                        entity.Property(e => e.Price)
                        .HasColumnType("decimal(18,2)")
                        .IsRequired(false);

                        entity.Property(e => e.RentalStartDate)
                                .IsRequired(false);

                        entity.Property(e => e.RentalEndDate)
                        .IsRequired(false);

                        entity.Property(e => e.IsDeleted)
                        .HasDefaultValue(false);

                        entity.Property(e => e.CreationTime)
                        .HasDefaultValueSql("GETDATE()");

                        entity.Property(e => e.DeletionTime)
                        .IsRequired(false);

                        entity.HasMany(a => a.Images)
                        .WithOne(ai => ai.Apartment)
                        .HasForeignKey(ai => ai.ApartmentId);

                  });
            }
      }
}
