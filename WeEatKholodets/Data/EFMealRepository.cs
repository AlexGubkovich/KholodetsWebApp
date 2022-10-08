using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async void AddMealAsync(string userId)
        {
            context.Meals.Add(new Meal
            {
                Date = DateTime.Now,
                User = await userManager.FindByIdAsync(userId)
            });
        }
        public List<Meal>? GetMealsByUserId(string userId)
        {
            return context.Meals.Where(m => m.User.Id == userId).ToList();
        }
    }
}
