using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using WeEatKholodets.Data;
using WeEatKholodets.Models;

namespace WeEatKholodets.Pages
{
    [Authorize]
    public class WantToEatModel : PageModel
    {
        private IMealRepository mealRepository;
        private UserManager<User> userManager;

        public WantToEatModel(IMealRepository mealRepository, UserManager<User> userManager)
        {
            this.userManager = userManager;
            this.mealRepository = mealRepository;
        }

        public bool DidCustomerEatToday = false;

        public void OnGetAsync()
        {
            var userId = userManager.GetUserId(User);
            Meal? lastMeal = mealRepository.GetLastMealByUserId(userId);
            if (lastMeal?.Date.Day == DateTime.Today.Day)
            {
                DidCustomerEatToday = true;
            }
        }

        public async Task<IActionResult> OnPost()
        {
            await mealRepository.AddMealAsync(HttpContext.User);
            return RedirectToPage();
        }
    }
}
