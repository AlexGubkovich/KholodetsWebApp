using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WeEatKholodets.Models;

namespace WeEatKholodets.Data
{
    public interface IMealRepository
    {
        IQueryable<Meal> GetMeals { get; }
        void AddMeal(User user);
        IQueryable<Meal> GetMealsByUserId(string userId);
        Task SaveAsync();
    }
}
