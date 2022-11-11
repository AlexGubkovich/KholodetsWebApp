using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WeEatKholodets.Data;

namespace WeEatKholodets.Pages.Recipes
{
    [Authorize]
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
        public PagingInfo PagingInfo { get; set; } = default!;
        private int PageSize = 2;

        public async Task OnGetAsync(int recipePage = 1, string? searchString = "")
        {
            if(string.IsNullOrEmpty(SearchString)){
                SearchString = searchString;
            }
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
                    .OrderByDescending(p => p.ViewCount)
                    .Skip((recipePage - 1) * PageSize)
                    .Take(PageSize)
                    .Select(r => new RecipeShort(r.Id, r.Title, r.Description, r.ViewCount))
                    .ToListAsync();
                PagingInfo = new PagingInfo{
                    CurrentPage = recipePage,
                    ItemsPerPage = PageSize,
                    TotalItems = string.IsNullOrEmpty(SearchString) ? context.Recipes.Count() : 
                        context.Recipes
                            .Where(s => s.Title.Contains(SearchString)).Count()
                };
            }
        }
    }
    public class PagingInfo {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }

        public int TotalPages =>
            (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
    }
    public record RecipeShort(int id, string title, string description, int viewCount);
}
