using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WeEatKholodets.Pages
{
    [AllowAnonymous]
    public class AboutModel : PageModel
    {
        private readonly ILogger<AboutModel> _logger;

        public AboutModel(ILogger<AboutModel> logger)
        {
            _logger = logger;
        }
        [ResponseCache(Location =ResponseCacheLocation.Any, Duration =300)]
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}