using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeEatKholodets.Models;

namespace WeEatKholodets.Data
{
    public interface IMealRepository
    {
        List<Meal>? GetMealsByUserId(string userId);
        void AddMealAsync(string userId);
    }
}
