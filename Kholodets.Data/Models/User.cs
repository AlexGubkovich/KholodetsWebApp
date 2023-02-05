using Microsoft.AspNetCore.Identity;

namespace Kholodets.Data.Models
{
    public class User : IdentityUser
    {
        public int Year { get; set; }
        public List<Meal>? Meals { get; set; }
        public List<Recipe> FavoriteRecipes { get; set; } = new List<Recipe>();
    }
}
