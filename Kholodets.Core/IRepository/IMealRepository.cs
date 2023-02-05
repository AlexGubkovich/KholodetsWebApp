using Kholodets.Data.Models;

namespace Kholodets.Core.IRepository
{
    public interface IMealRepository
    {
        IQueryable<Meal> GetMeals { get; }
        void AddMeal(User user);
        IQueryable<Meal> GetMealsByUserId(string userId);
        void RemoveRangeMeals(IEnumerable<Meal> meals);
        Task SaveAsync();
    }
}
