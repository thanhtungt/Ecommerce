using Data.Entity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Data.Entity.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    Id = 1,
                    Name = "Foo",
                    SortOrder = 1,
                    IsShowOnHome = true,
                },
                new Category()
                {
                    Id = 2,
                    Name = "Bar",
                    SortOrder = 2,
                    IsShowOnHome = true,
                },
                new Category()
                {
                    Id= 3,
                    Name = "SubFoo1",
                    SortOrder = 1,
                    IsShowOnHome = true,
                    ParentId = 1,
                },
                new Category()
                {
                    Id = 4,
                    Name = "SubFoo2",
                    SortOrder = 2,
                    IsShowOnHome = true,
                    ParentId= 1,
                },
                new Category()
                {
                    Id = 5,
                    Name = "SubBar1",
                    SortOrder = 1,
                    IsShowOnHome = true,
                    ParentId= 2,
                },
                new Category()
                {
                    Id = 6,
                    Name = "SubBar1",
                    SortOrder = 2,
                    IsShowOnHome = true,
                    ParentId= 2,
                }
             );
            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    Id = 1,
                    Name = "Product test 1",
                    Description = "Product description 1",
                    Price = 100000,
                    DiscountPercent = 0,
                    Stock = 10,
                    CreateAt = DateTime.Now,
                    IsFeatured = true,
                },
                new Product()
                {
                    Id = 2,
                    Name = "Product test 2",
                    Description = "Product description 2",
                    Price = 200000,
                    DiscountPercent = 10,
                    Stock = 20,
                    CreateAt = DateTime.Now,
                    IsFeatured = false,
                }
                );
            modelBuilder.Entity<ProductInCategory>().HasData(
                new ProductInCategory()
                {
                    ProductId = 1,
                    CategoryId = 3,
                },
                new ProductInCategory()
                {
                    ProductId = 2,
                    CategoryId = 2,
                },
                new ProductInCategory()
                {
                    ProductId = 1,
                    CategoryId = 2,
                }
                );

            var roleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC");
            var adminId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE");
            var hasher = new PasswordHasher<AppUser>();

            modelBuilder.Entity<AppRole>().HasData(
                new AppRole()
                {
                    Id = roleId,
                    Name = "admin",
                    NormalizedName = "ADMIN",
                }
                );

            modelBuilder.Entity<AppUser>().HasData(

                new AppUser()
                {
                    Id = adminId,
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = "anhvu.siron@gmail.com",
                    NormalizedEmail = "ANHVU.SIRON@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Abcd1234$"),
                    SecurityStamp = string.Empty,
                    FirstName = "Anh",
                    LastName = "Vu",
                    Dob = new DateTime(2003, 10, 06)
                });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(
                new IdentityUserRole<Guid>()
                {
                    RoleId = roleId,
                    UserId = adminId,
                });
        }
    }
}
