using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;    
using MyBiome.Models;

namespace MyBiome.Infrastructure
{
        public class DataContext : IdentityDbContext<AppUser>
        {
                public DataContext(DbContextOptions<DataContext> options) : base(options)
                { }

		public DbSet<Category> Categories { get; set; }

        public DbSet<Employees> Employees { get; set; }

        public DbSet<Customers> Customers { get; set; }

        public DbSet<Orders> Orders { get; set; }

        public DbSet<OrderItems> OrderItems { get; set; }

        public DbSet<Products> Products { get; set; }

        public DbSet<SubCategory> SubCategories { get; set; }

        public DbSet<Suppliers> Suppliers { get; set; }

        public DbSet<Shippers> Shippers { get; set; }

        public DbSet<Favorites> Favorites { get; set; }


    }
}
