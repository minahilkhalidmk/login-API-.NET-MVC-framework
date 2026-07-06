using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using APIlogin.Models; // Add this using statement

namespace APIlogin.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Tell EF Core to create a 'Products' table in SQL Server
        public DbSet<Product> Products { get; set; }
    }
}