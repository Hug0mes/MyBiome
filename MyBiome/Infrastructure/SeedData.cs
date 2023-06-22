using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyBiome.Models;
using System;

namespace MyBiome.Infrastructure
{
    public class SeedData
    {
        public static async Task SeedDatabase(DataContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Gera a bd
            context.Database.Migrate();

            await SeedRoles(context, roleManager);
            await SeedUsers(context, userManager);

            await SeedModel(context);

        }


        private static async Task SeedRoles(DataContext context, RoleManager<IdentityRole> roleManager)
        {
            string[] roles = new string[] { "Admin", "Customer","Super Admin" };

            foreach (string role in roles)
            {
                if (!context.Roles.Any(r => r.Name == role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        private static async Task SeedUsers(DataContext context, UserManager<AppUser> userManager)
        {
            var user = new AppUser
            {
                UserName = "admin",
                Email = "2005hugogomes@gmail.com",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            if (!context.Users.Any(u => u.UserName == user.UserName))
            {
                await userManager.CreateAsync(user, "123");
                //await userManager.AddToRoleAsync(user, "Admin5");
            }
        }
        private static async Task SeedModel(DataContext context)
        {
            if (!context.Products.Any())
            {
                Category Plants = new Category { Name = "Plants", Image = "cloth_1.jpg" };
                SubCategory rose = new SubCategory { Name = "Rose", Category = Plants, };


                for (int i = 1; i <= 10; i++)
                {
                    // Crie um novo produto
                    Products newProduct = new Products
                    {
                        Name = "Ficus Lyrata Plant " + i.ToString(),
                        Description = "This is a popular and stylish indoor plant with large, shiny leaves shaped like a violin. The plant has an average height of 1 meter and is native to West Africa. The Ficus Lyrata is an easy-to-care-for plant, and is an excellent choice for anyone looking to add a tropical and exotic plant to their home.",
                        Price = 2*i,
                        Status = "Active",
                        Height = "30",
                        Whidh = "10",
                        Stock = "5",
                        Image1 = "repeat.png",
                        Image2 = "repeat.png",
                        Image3 = "sock-1-min.jpg",
                        SubCategory = rose
                    };


                    context.Products.Add(newProduct);
                    await context.SaveChangesAsync();
                }

            }


        }
    }
}