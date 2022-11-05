using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WeEatKholodets.Data;
using WeEatKholodets.Models;

namespace WeEatKholodets.Pages.Recipes
{
    public class TopModel : PageModel
    {
        private readonly ApplicationDbContext context;
        public TopModel(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Recipe? Recipe { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? Id)
        {
            if (Id == null || context.Recipes == null)
            {
                return NotFound();
            }

            Recipe = await context.Recipes.FindAsync(Id);
            if(Recipe == null)
            {
                return NotFound();
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
