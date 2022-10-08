using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WeEatKholodets.Models;

namespace WeEatKholodets.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
        public DbSet<Meal> Meals { get; set; } = null!;
        public new DbSet<User> Users { get; set; } = null!;
    }
}