using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WeEatKholodets.Data;
using WeEatKholodets.Models;

namespace WeEatKholodets.Pages.Recipes
{
    public class RecipeDetails : PageModel
    {
        private readonly ApplicationDbContext context;
        public RecipeDetails(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Recipe? Recipe { get; set; } = default!;
        public List<string> Images { get; set; } = new List<string>();

        public async Task<IActionResult> OnGetAsync(int? Id)
        {
            if (Id == null || context.Recipes == null)
            {
                return NotFound();
            }

            Recipe = await context.Recipes.Include(r => r.Photos).FirstAsync(r => r.Id == Id);
            if(Recipe == null)
            {
                return NotFound();
            }
            
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

            return Page();
        }
    }
}
