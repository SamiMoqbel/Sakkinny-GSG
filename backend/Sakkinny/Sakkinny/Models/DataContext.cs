using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Sakkinny.Models
{
	public class DataContext : IdentityDbContext<ApplicationUser>
	{
		public DbSet<Apartment>? Apartments { get; set; } = null;
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

                entity.Property(e => e.title)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.subTitle)
                      .HasMaxLength(500);

                entity.Property(e => e.location)
                      .IsRequired()
                      .HasMaxLength(150);

                entity.Property(e => e.roomsNumber)
                      .IsRequired(false);

                entity.Property(e => e.roomsAvailable)
                      .IsRequired(false);

                entity.Property(e => e.price)
                      .HasColumnType("decimal(18,2)")
                      .IsRequired(false);

                /*entity.Property(e => e.pictureUrls)
                      .HasConversion(
                          v => string.Join(',', v),
                          v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                      );*/

                entity.Property(e => e.IsDeleted)
                      .HasDefaultValue(false);

                entity.Property(e => e.CreationTime)
                      .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.DeletionTime)
                      .IsRequired(false);
            });

        }


    }
}
