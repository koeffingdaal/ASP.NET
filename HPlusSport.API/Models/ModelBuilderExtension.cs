using Microsoft.EntityFrameworkCore;

namespace HPlusSport.API.Models
{
    public static class ModelBuilderExtension
    {
        public static void Seed (this ModelBuilder modelBuilder)
        {
            // Define your seeding logic here
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Category 1" },
                new Category { Id = 2, Name = "Category 2" }
                
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Product 1", CategoryId = 1 },
                new Product { Id = 2, Name = "Product 2", CategoryId = 1 },
                new Product { Id = 3, Name = "Product 3", CategoryId = 2 }
                
            );
        }
    }
}
