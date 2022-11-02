using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WeEatKholodets.Data;
using WeEatKholodets.Models;

namespace WeEatKholodets.Pages.Recipes
{
    public class UploadRecipeModel : PageModel
    {
        private readonly ApplicationDbContext context;

        [BindProperty]
        public Recipe Recipe { get; set; } = null!;

        public UploadRecipeModel(ApplicationDbContext context)
        {
            this.context = context;
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
            context.Recipes.Add(Recipe);
            context.SaveChanges();

            return RedirectToPage();
        }
    }
}
