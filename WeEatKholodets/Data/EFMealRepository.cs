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

        public EFMealRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Meal> GetMeals => context.Meals;

        public void AddMeal(User user)
        {
            context.Meals.Add(new Meal
            {
                Date = DateTime.Now,
                User =  user
            });
        }

        IQueryable<Meal> IMealRepository.GetMealsByUserId(string userId)
        {
            IQueryable<Meal> meals = context.Meals.Where(m => m.User.Id == userId);
            return meals;
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
        // public Meal? GetLastMealByUserId(string userId)
        // {
        //     var meals = context.Meals.Where(m => m.User.Id == userId).ToList();
        //     if(meals.Count > 0)
        //     {
        //         return meals.Last();
        //     }
        //     else
        //     {
        //         return meals.FirstOrDefault();
        //     }
        // }

