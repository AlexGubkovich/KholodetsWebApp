using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kholodets.Data.Models
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "TitleRequired")]
        [Display(Name = "Title")]
        [StringLength(20, ErrorMessage = "TitleMaxString")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "DescriptionRequired")]
        [Display(Name = "Description")]
        [StringLength(60, ErrorMessage = "DescriptionMaxString")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "IngredientsRequired")]
        [Display(Name = "Ingredients")]
        public string Ingredients { get; set; } = string.Empty;

        [Required(ErrorMessage = "CookingMethodRequired")]
        [Display(Name = "Cooking method")]
        [StringLength(5000, ErrorMessage = "CookingMethodMaxString")]
        public string CookingMethod { get; set; } = string.Empty;

        public int ViewCount { get; set; }

        public List<Photo>? Photos { get; set; } = null!;

        [FromForm]
        [NotMapped]
        [Required(ErrorMessage = "PhotosRequired")]
        [Display(Name = "Photos")]
        public IFormFileCollection? Files { get; set; } = null!;

        public List<User> Users { get; set; } = new List<User>();
    }
}
