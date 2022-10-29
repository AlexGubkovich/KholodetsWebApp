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
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        [Required]
        public string Ingredients { get; set; } = null!;

        [Required]
        public string CookingMethod { get; set; } = null!;

        public int NumberOfLikes { get; set; }

        public List<Photo>? Photos { get; set; } = null!;

        [FromForm]
        [NotMapped]
        public IFormFileCollection? Files { get; set; } = null!;
    }
}
