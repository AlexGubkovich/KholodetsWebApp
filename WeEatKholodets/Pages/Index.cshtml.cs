using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeEatKholodets.Data;

namespace WeEatKholodets.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        IMealRepository mealRepository;

        public IndexModel(ILogger<IndexModel> logger, IMealRepository mealRepository)
        {
            this.mealRepository = mealRepository;
            _logger = logger;
        }
        [ResponseCache(Location =ResponseCacheLocation.Any, Duration =300)]
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}