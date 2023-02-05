using Kholodets.Core.IRepository;
using Kholodets.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kholodets.App.Pages.Recipes
{
    [Authorize]
    public class UploadRecipeModel : PageModel
    {
        private readonly IRecipeRepository recipeRepository;

        [BindProperty]
        public Recipe Recipe { get; set; } = null!;

        public UploadRecipeModel(IRecipeRepository recipeRepository)
        {
            this.recipeRepository = recipeRepository;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            List<Photo> photos = new List<Photo>();
            if (Recipe.Files?.Count > 0)
            {
                foreach (var formFile in Recipe.Files)
                {
                    if (formFile.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await formFile.CopyToAsync(memoryStream);
                            if (memoryStream.Length < 2097152)
                            {
                                var newPhoto = new Photo
                                {
                                    Bytes = memoryStream.ToArray()
                                };
                                photos.Add(newPhoto);
                            }
                            else
                            {
                                ModelState.AddModelError("File", "The file is too large");
                            }
                        }
                    }
                }
            }
            Recipe.Photos = photos;
            recipeRepository.AddRecipe(Recipe);
            await recipeRepository.SaveAsync();

            return RedirectToPage("/Recipes/Index");
        }
    }
}
