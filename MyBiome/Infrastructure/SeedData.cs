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
            string[] roles = new string[] { "Admin", "Customer" };

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
                Email = "admin@example.com",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            if (!context.Users.Any(u => u.UserName == user.UserName))
            {
                await userManager.CreateAsync(user, "123Pa$$word.");
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }

        private static async Task SeedModel(DataContext context)
        {
            if (!context.Products.Any())
            {
                Category fruits = new Category { Name = "Fruits" };
                Category shirts = new Category { Name = "Shirts" };

                context.Products.AddRange(
                        new Products
                        {
                            Name = "Apples",
                            Description = "Juicy apples"
                        }
                        
                );

                await context.SaveChangesAsync();
            }

        }


    }
}