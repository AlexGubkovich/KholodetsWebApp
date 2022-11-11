using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WeEatKholodets.Data;
using WeEatKholodets.Models;

namespace WeEatKholodets.Pages.Recipes
{
    [Authorize]
    public class RecipeDetails : PageModel
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;
        public RecipeDetails(ApplicationDbContext context, UserManager<User> userManager)
        {
            this.userManager = userManager;
            this.context = context;
        }

        public Recipe? Recipe { get; set; } = default!;
        public List<string> Images { get; set; } = new List<string>();

        public async Task<IActionResult> OnGetAsync(int? Id)
        {
            Recipe = await context.Recipes.Include(r => r.Photos).FirstOrDefaultAsync(r => r.Id == Id);
            if(Recipe != null)
            {
                string base64;
                string image; 
                foreach(var elem in Recipe.Photos!){
                    base64 = Convert.ToBase64String(elem.Bytes);
                    image = String.Format("data:image/gif;base64,{0}", base64);
                    Images.Add(image);
                }

                string queryString = HttpContext.Request.QueryString.ToString();
                if(HttpContext.Session.GetString("VisitePage") != queryString){
                    Recipe.ViewCount++;
                    HttpContext.Session.SetString("VisitePage", queryString);
                }

                context.Recipes.Update(Recipe);
                await context.SaveChangesAsync();
            }

            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int recipeId){
            var user = await userManager.GetUserAsync(HttpContext.User);
            context.Users.Include(p => p.FavoriteRecipes).FirstOrDefault(p => p.Id == user.Id);
            if(user != null)
            {
                var recipe = context.Recipes.Find(recipeId);
                if(user.FavoriteRecipes.Find(p => p.Id == recipeId) == null){
                     recipe!.Users.Add(user);
                     await context.SaveChangesAsync();
                } else {
                    recipe!.Users.Remove(user);
                    await context.SaveChangesAsync();
                }
            }

            return RedirectToPage(new { Id = recipeId });
        }
    }
}
