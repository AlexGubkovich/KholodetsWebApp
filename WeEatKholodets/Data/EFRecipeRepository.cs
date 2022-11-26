using WeEatKholodets.Models;

namespace WeEatKholodets.Data;

public class EFRecipeRepository : IRecipeRepository
{
    private readonly ApplicationDbContext context;

    public EFRecipeRepository(ApplicationDbContext context){
        this.context = context;
    }

    public IQueryable<Recipe> GetRecipes => context.Recipes;

    public void AddRecipe()
    {
        throw new NotImplementedException();
    }

    public async Task<Recipe?> GetRecipeAsync(int recipeId)
    {
        return await context.Recipes.FindAsync(recipeId);
    }
}