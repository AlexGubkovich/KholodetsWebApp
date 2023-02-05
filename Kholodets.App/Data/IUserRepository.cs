namespace Kholodets.App.Data;

using Kholodets.App.Models;

public interface IUserRepository
{
    IQueryable<User> GetUsers { get; }
    Task<User?> GetUserAsync(string userId);
    void RevoveFavoriteRecipe(Recipe recipe, User user);
    Task SaveAsync();
    // void UpdateRecipe(Recipe recipe); 
    // void AddRecipe();
    // Task SaveAsync();
}