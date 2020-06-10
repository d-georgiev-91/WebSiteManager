using System;
using System.Linq;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using WebSiteManager.DataModels;

namespace WebSiteManager.Data
{
    public class WebSiteManagerContext : DbContext
    {
        public DbSet<WebSite> WebSites { get; set; }

        public DbSet<Login> Logins { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=WebSiteManager;Trusted_Connection=True;MultipleActiveResultSets=true");
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Category>(entityTypeBuilder =>
                {
                    entityTypeBuilder.Property(e => e.Id)
                        .HasConversion<byte>();

                    entityTypeBuilder.Property(e => e.Name)
                        .IsRequired()
                        .HasMaxLength(100);

                    entityTypeBuilder.HasData(Enum.GetValues(typeof(CategoryId))
                        .Cast<CategoryId>()
                        .Select(e => new Category
                        {
                            Id = e,
                            Name = e.ToString().Humanize(LetterCasing.Sentence)
                        })
                    );
                });

            modelBuilder
                .Entity<WebSite>(entityTypeBuilder =>
                {
                    entityTypeBuilder.Property(e => e.Name)
                        .IsRequired()
                        .HasMaxLength(100);

                    entityTypeBuilder.Property(e => e.Url)
                        .IsRequired()
                        .HasMaxLength(200);

                    entityTypeBuilder.Property(e => e.HomePageSnapshotPath)
                        .IsRequired()
                        .HasMaxLength(300);

                    entityTypeBuilder.Property(e => e.IsDeleted)
                        .IsRequired()
                        .HasDefaultValue(false);

                    entityTypeBuilder.Property(e => e.CategoryId)
                        .HasConversion<byte>();
                });

            modelBuilder
                .Entity<Login>(entityTypeBuilder =>
                {
                    entityTypeBuilder.Property(e => e.Username)
                        .IsRequired()
                        .HasMaxLength(100);

                    entityTypeBuilder.Property(e => e.Password)
                        .IsRequired()
                        .HasMaxLength(100);
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
