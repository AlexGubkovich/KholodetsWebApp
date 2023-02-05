using Kholodets.Core.IRepository;
using Kholodets.Data;
using Kholodets.Data.Models;

namespace Kholodets.Core.Repository
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
                User = user
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

        public void RemoveRangeMeals(IEnumerable<Meal> meals)
        {
            context.Meals.RemoveRange(meals);
        }
    }
}
