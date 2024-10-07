using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Sakkinny.Models
{
      public class DataContext : IdentityDbContext<ApplicationUser>
      {
            public DbSet<Apartment> Apartments { get; set; } = null!; // Not nullable
            public DbSet<Renter> Renters { get; set; }
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
                        entity.Property(e => e.OwnerId)
                        .IsRequired().HasMaxLength(450);

                        entity.HasMany(a => a.Images)
                            .WithOne(ai => ai.Apartment)
                            .HasForeignKey(ai => ai.ApartmentId);

                        entity.HasMany(a => a.Renters)
                              .WithOne(r => r.Apartment)
                              .HasForeignKey(r => r.ApartmentId);

                  });
                  modelBuilder.Entity<Renter>(entity =>
            {
                  entity.HasKey(e => e.Id);
                  entity.Property(e => e.UserId).IsRequired();
                  entity.Property(e => e.ApartmentId).IsRequired();

                  entity.HasOne(r => r.User)
                        .WithMany() // User can have many Renters, but no back reference in this case
                        .HasForeignKey(r => r.UserId)
                        .OnDelete(DeleteBehavior.Cascade); // Optional: Define delete behavior

                  entity.HasOne(r => r.Apartment)
                        .WithMany(a => a.Renters) // Apartment can have many Renters
                        .HasForeignKey(r => r.ApartmentId)
                        .OnDelete(DeleteBehavior.Cascade); // Optional: Define delete behavior
            });



            }
      }
}
