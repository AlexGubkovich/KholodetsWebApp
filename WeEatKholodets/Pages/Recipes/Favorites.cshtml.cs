using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WeEatKholodets.Data;
using WeEatKholodets.Models;

namespace WeEatKholodets.Pages.Recipes
{
    public class FavoritesModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly ApplicationDbContext context;
        public FavoritesModel(UserManager<User> userManager, ApplicationDbContext context){
            this.userManager = userManager;
            this.context = context;
        }

        public List<Recipe> FavoriteRecipes { get; set; } = new List<Recipe>();

        public async Task OnGetAsync()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            context.Users.Include(p => p.FavoriteRecipes).FirstOrDefault(p => p.Id == user.Id);
            if(user.FavoriteRecipes != null){
                FavoriteRecipes = user.FavoriteRecipes.ToList();
            }
        }

        public async Task OnPost(){
            
        }
    }
}