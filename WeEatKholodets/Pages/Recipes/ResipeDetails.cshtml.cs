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

            Recipe.ViewCount++;
            context.Recipes.Update(Recipe);
            await context.SaveChangesAsync();

            return Page();
        }
    }
}
