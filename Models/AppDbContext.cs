using Microsoft.EntityFrameworkCore;

namespace ProjectMVC.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Category> categories { get; set; }
        public DbSet<Product> Products { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=JAWADHAMDAN;Database=ProjectMVC;Trusted_connection=True;TrustServerCertificate=True");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1,Name="phones",Description="new Phones" },
                new Category { Id = 2,Name="Laptops",Description="new and used" },
                new Category { Id = 3,Name="Washing Machines",Description="All" }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product {Id=1,Name="iphone 8 plus",Price=1000,CategoryId=1 },
                new Product { Id = 2, Name = "iphone 13 pro max", Price = 2000, CategoryId = 1 },
                new Product { Id = 3, Name = "Smasung ultra 21", Price = 3000, CategoryId = 1 },
                new Product { Id = 4, Name = "Msi", Price = 3000, CategoryId = 2 },
                new Product { Id = 5, Name = "Lenovo", Price = 4000, CategoryId = 2 },
                new Product { Id = 6, Name = "Samsung", Price = 1000, CategoryId = 3 }
                
                
                );

        }
    }
}
