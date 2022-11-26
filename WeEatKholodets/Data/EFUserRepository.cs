using Microsoft.EntityFrameworkCore;
using WeEatKholodets.Models;

namespace WeEatKholodets.Data;

public class EFUserRepository : IUserRepository{
    private readonly ApplicationDbContext context;

    public EFUserRepository(ApplicationDbContext context){
        this.context = context;
    }

    public IQueryable<User> GetUsers => context.Users;

    public async Task<User?> GetUserAsync(string userId)
    {
        return await context.Users.FindAsync(userId);
    }

    public void RevoveFavoriteRecipe(Recipe recipe, User user)
    {
        context.Users.Find(user.Id)!.FavoriteRecipes.Remove(recipe);
    }

    public async Task SaveAsync()
    {
        await context.SaveChangesAsync();
    }
}