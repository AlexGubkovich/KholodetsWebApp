namespace Kholodets.Core.IRepository;

using Kholodets.Data.Models;

public interface IRecipeRepository
{
    IQueryable<Recipe> GetRecipes { get; }
    Task<Recipe?> GetRecipeAsync(int recipeId);
    void UpdateRecipe(Recipe recipe);
    void AddRecipe(Recipe recipe);
    Task SaveAsync();
}