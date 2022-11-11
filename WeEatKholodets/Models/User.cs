using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeEatKholodets.Models
{
    public class User : IdentityUser
    {
        // [Key]
        // public new string Id { get; set; } = null!;
        public int Year { get; set; }
        public List<Meal>? Meals { get; set; }
        public List<Recipe> FavoriteRecipes { get; set; } = new List<Recipe>();
    }
}
