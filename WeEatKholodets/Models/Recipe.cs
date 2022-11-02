using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeEatKholodets.Models
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        public string Ingredients { get; set; } = string.Empty;

        [Required]
        public string CookingMethod { get; set; } = string.Empty;

        public int NumberOfViews { get; set; }

        public List<Photo>? Photos { get; set; } = null!;

        [FromForm]
        [NotMapped]
        public IFormFileCollection? Files { get; set; } = null!;
    }
}
