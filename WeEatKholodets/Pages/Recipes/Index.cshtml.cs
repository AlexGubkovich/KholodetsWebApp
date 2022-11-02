using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WeEatKholodets.Data;

namespace WeEatKholodets.Pages.Recipes
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext context;
        public IndexModel(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<RecipeShort> RecipeShorts { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public bool IsTop { get; set; } = true;

        public async Task OnGetAsync()
        {
            if(context.Recipes != null)
            {
                var recipeShorts = from r in context.Recipes
                                   select r;
                if(!string.IsNullOrEmpty(SearchString))
                {
                    recipeShorts = recipeShorts.Where(s => s.Title.Contains(SearchString));
                    IsTop = false;
                }
                RecipeShorts = await recipeShorts
                    .OrderBy(p => p.NumberOfViews)
                    .Take(5)
                    .Select(r => new RecipeShort(r.Id, r.Title, r.NumberOfViews))
                    .ToListAsync();
            }
        }
    }

    public record RecipeShort(int id, string title, int numberOfViews);
}
