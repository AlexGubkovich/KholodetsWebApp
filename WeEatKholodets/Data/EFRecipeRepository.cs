using WeEatKholodets.Models;

namespace WeEatKholodets.Data;

public class EFRecipeRepository : IRecipeRepository
{
    private readonly ApplicationDbContext context;

    public EFRecipeRepository(ApplicationDbContext context){
        this.context = context;
    }

    public IQueryable<Recipe> GetRecipes => context.Recipes;

    public void AddRecipe(Recipe recipe)
    {
        if(recipe != null){
            context.Recipes.Add(recipe);
        }
    }

    public async Task<Recipe?> GetRecipeAsync(int recipeId)
    {
        return await context.Recipes.FindAsync(recipeId);
    }

    public async Task SaveAsync()
    {
        await context.SaveChangesAsync();
    }

    public void UpdateRecipe(Recipe recipe)
    {
        if(recipe != null){
            context.Recipes.Update(recipe);
        }
    }
}