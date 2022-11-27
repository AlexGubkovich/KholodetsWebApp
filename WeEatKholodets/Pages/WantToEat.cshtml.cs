using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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

        public List<Meal> Meals { get; set; } = new List<Meal>();

        public void OnGet()
        {
            var userId = userManager.GetUserId(User);

            Meal? lastMeal;
            var mealsCount = mealRepository.GetMealsByUserId(userId).Count();
            if(mealsCount > 0) {
                lastMeal = mealRepository.GetMealsByUserId(userId).OrderBy(m => m.Date).Last();
            } else {
                lastMeal = null;
            }
            if (lastMeal != null && lastMeal?.Date.Day == DateTime.Today.Day)
            {
                DidCustomerEatToday = true;
            }

            Meals = mealRepository.GetMeals.OrderBy(m => m.Date).Take(20).Include(m => m.User).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            User user = await userManager.GetUserAsync(HttpContext.User);
            mealRepository.AddMeal(user);
            await mealRepository.SaveAsync();

            return RedirectToPage();
        }
    }
}
