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
        private readonly IUserRepository userRepository;
        public Bookmark(UserManager<User> userManager, IUserRepository userRepository){
            this.userManager = userManager;
            this.userRepository = userRepository;
        }
        
        public async Task<IViewComponentResult> InvokeAsync(int recipeId){
            bool isItFavorite = false;
            var user = await userManager.GetUserAsync(HttpContext.User);
            userRepository.GetUsers.Include(p => p.FavoriteRecipes).FirstOrDefault(p => p.Id == user.Id);
            if(user.FavoriteRecipes != null)
                if(user.FavoriteRecipes.Find(p => p.Id == recipeId) != null)
                    isItFavorite = true;
            return View(isItFavorite);
        }
    }
}