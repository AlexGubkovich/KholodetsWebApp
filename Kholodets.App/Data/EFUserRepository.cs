using Kholodets.App.Models;
using Microsoft.EntityFrameworkCore;

namespace Kholodets.App.Data;

public class EFUserRepository : IUserRepository
{
    private readonly ApplicationDbContext context;

    public EFUserRepository(ApplicationDbContext context)
    {
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