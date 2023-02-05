using Kholodets.App.Data;
using Kholodets.App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Kholodets.App.Pages.Recipes
{
    [Authorize]
    public class FavoritesModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly IRecipeRepository recipeRepository;
        private readonly IUserRepository userRepository;
        public FavoritesModel(UserManager<User> userManager, IUserRepository userRepository, IRecipeRepository recipeRepository)
        {
            this.userManager = userManager;
            this.userRepository = userRepository;
            this.recipeRepository = recipeRepository;
        }

        public List<Recipe> FavoriteRecipes { get; set; } = new List<Recipe>();

        public async Task OnGetAsync()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            userRepository.GetUsers.Include(p => p.FavoriteRecipes).FirstOrDefault(p => p.Id == user.Id);
            if (user.FavoriteRecipes != null)
            {
                FavoriteRecipes = user.FavoriteRecipes.ToList();
            }
        }

        public async Task<IActionResult> OnPost(int recipeId)
        {
            var recipe = await recipeRepository.GetRecipeAsync(recipeId);
            if (recipe != null)
            {
                var user = await userManager.GetUserAsync(HttpContext.User);
                userRepository.GetUsers.Include(p => p.FavoriteRecipes).FirstOrDefault(p => p.Id == user.Id);
                userRepository.RevoveFavoriteRecipe(recipe, user);
                await userRepository.SaveAsync();
            }

            return RedirectToPage();
        }
    }
}