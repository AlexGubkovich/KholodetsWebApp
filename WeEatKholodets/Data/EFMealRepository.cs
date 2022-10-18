using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WeEatKholodets.Data;
using WeEatKholodets.Models;

namespace WeEatKholodets.Data
{
    public class EFMealRepository : IMealRepository
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;
        public EFMealRepository(ApplicationDbContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task AddMealAsync(ClaimsPrincipal claimsPrincipal)
        {
            context.Meals.Add(new Meal
            {
                Date = DateTime.Now,
                User = await userManager.GetUserAsync(claimsPrincipal)
            });
            await context.SaveChangesAsync();
        }
        public List<Meal>? GetMealsByUserId(string userId)
        {
            IQueryable<Meal> meals = context.Meals.Where(m => m.User.Id == userId);
            return meals.ToList();
        }

        public Meal? GetLastMealByUserId(string userId)
        {
            var meals = context.Meals.Where(m => m.User.Id == userId).ToList();
            if(meals.Count > 0)
            {
                return meals.Last();
            }
            else
            {
                return meals.FirstOrDefault();
            }
        }

        public List<Meal> GetAllMeals()
        {
            return context.Meals.ToList();
        }
    }
}
