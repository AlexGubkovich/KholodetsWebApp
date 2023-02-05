using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json;
using Kholodets.App.Models;

namespace Kholodets.App.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
        public DbSet<Meal> Meals { get; set; } = null!;
        public new DbSet<User> Users { get; set; } = null!;
        public DbSet<Recipe> Recipes { get; set; } = null!;
        public DbSet<Photo> Photos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Recipe>()
                .HasMany(p => p.Users)
                .WithMany(p => p.FavoriteRecipes)
                .UsingEntity(j => j.ToTable("UserRecipes"));
        }
    }
}