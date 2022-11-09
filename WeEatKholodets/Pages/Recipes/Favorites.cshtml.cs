using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeEatKholodets.Models;

namespace WeEatKholodets.Pages.Recipes
{
    public class FavoritesModel : PageModel
    {
        private readonly UserManager<User> userManager;
        public FavoritesModel(UserManager<User> userManager){
            this.userManager = userManager;
        }

        public List<Recipe> FavoriteRecipes { get; set; } = new List<Recipe>();

        public async Task OnGetAsync()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            if(user.FavoriteRecipe != null)
                FavoriteRecipes = user.FavoriteRecipe.ToList();
        }
    }
}