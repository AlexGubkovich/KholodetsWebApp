using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeEatKholodets.Data;
using WeEatKholodets.Models;

namespace WeEatKholodets.Components
{
    public class Bookmark : ViewComponent
    {
        private readonly UserManager<User> userManager;
        private readonly ApplicationDbContext context;
        public Bookmark(UserManager<User> userManager, ApplicationDbContext context){
            this.userManager = userManager;
            this.context = context;
        }
        
        public async Task<IViewComponentResult> InvokeAsync(int recipeId){
            bool isItFavorite = false;
            var user = await userManager.GetUserAsync(HttpContext.User);
            context.Users.Include(p => p.FavoriteRecipes).FirstOrDefault(p => p.Id == user.Id);
            if(user.FavoriteRecipes != null)
                if(user.FavoriteRecipes.Find(p => p.Id == recipeId) != null)
                    isItFavorite = true;
            return View(isItFavorite);
        }
    }
}