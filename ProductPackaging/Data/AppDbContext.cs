using Microsoft.EntityFrameworkCore;
using ProductPackaging.Entities;

namespace ProductPackaging.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Packaging> Packaging => Set<Packaging>();
        public DbSet<PackagingType> PackagingTypes => Set<PackagingType>();
        public DbSet<Item> Items => Set<Item>();
        public DbSet<PackagingItem> PackagingItems => Set<PackagingItem>();
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Product
            modelBuilder.Entity<Product>()
                .HasKey(p => p.ProductId);

            // Packaging
            modelBuilder.Entity<Packaging>()
                .HasKey(p => p.PackageId);

            modelBuilder.Entity<Packaging>()
                .HasOne(p => p.Product)
                .WithMany(p => p.Packages)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Packaging>()
                .HasOne(p => p.PackageType)
                .WithMany(pt => pt.Packages)
                .HasForeignKey(p => p.PackageTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Packaging>()
                .HasOne(p => p.Parent)
                .WithMany(p => p.Children)
                .HasForeignKey(p => p.ParentPackageId)
                .OnDelete(DeleteBehavior.Restrict); // prevents cascade loop

            // PackagingType
            modelBuilder.Entity<PackagingType>()
                .HasKey(pt => pt.PackageTypeId);

            // Item
            modelBuilder.Entity<Item>()
                .HasKey(i => i.ItemId);

            // PackagingItem (Many-to-Many Bridge)
            modelBuilder.Entity<PackagingItem>()
                .HasKey(pi => new { pi.PackageId, pi.ItemId });

            modelBuilder.Entity<PackagingItem>()
                .HasOne(pi => pi.Package)
                .WithMany(p => p.PackagingItems)
                .HasForeignKey(pi => pi.PackageId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PackagingItem>()
                .HasOne(pi => pi.Item)
                .WithMany(i => i.PackagingItems)
                .HasForeignKey(pi => pi.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            // User
            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique(); // prevent duplicate usernames
        }
    }
}
