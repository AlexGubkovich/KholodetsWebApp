using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json;
using WeEatKholodets.Models;

namespace WeEatKholodets.Data
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

            //builder.Entity<Recipe>().Property(p => p.Ingredients)
            //    .HasConversion(
            //        v => JsonConvert.SerializeObject(v),
            //        v => (List<string>)JsonConvert.DeserializeObject(v)!);
        }
    }
}