using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;
using WebBanHang.Models;

namespace WebBanHang.Repository
{
    public class SeedData
    {
        public static void SeedingData(DataContext _context)
        {
            _context.Database.Migrate();

            if (!_context.Products.Any())
            {
                CategoryModel macbook = new CategoryModel
                {
                    Name = "Macbook",
                    Slug = "macbook",
                    Description = "Apple is Large Brank in the world",
                    Status = 1
                };

                CategoryModel pc = new CategoryModel
                {
                    Name = "Pc",
                    Slug = "pc",
                    Description = "Samsung is Large Brank in the world",
                    Status = 1
                };

                BrandModel apple = new BrandModel
                {
                    Name = "Apple",
                    Slug = "apple",
                    Description = "Apple is Large Brank in the world",
                    Status = 1
                };

                BrandModel samsung = new BrandModel
                {
                    Name = "Samsung",
                    Slug = "SS",
                    Description = "Samsung is Large Brank in the world",
                    Status = 1
                };

                Console.WriteLine("ADD");

                _context.Products.AddRange(
                    new ProductModel { Name = "Macbook", Slug = "macbook", Description = "Macbook is best", Image = "1.jpg", Category = macbook, Price = 1233, Brand = apple },
                    new ProductModel { Name = "MSI Pc", Slug = "msi", Description = "PCCCC", Image = "2.jpg", Category = pc, Brand = samsung, Price = 1000 }
                );

                _context.SaveChanges();
            }
        }
    }
}