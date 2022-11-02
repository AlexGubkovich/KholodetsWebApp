using System.ComponentModel.DataAnnotations.Schema;

namespace WeEatKholodets.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public byte[] Bytes { get; set; } = null!;

        public int RecipeId { get; set; }
        [ForeignKey("RecipeId")]
        public Recipe Recipe { get; set; } = null!;

    }
}
