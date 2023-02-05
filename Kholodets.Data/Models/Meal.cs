using System.ComponentModel.DataAnnotations;

namespace Kholodets.Data.Models
{
    public class Meal
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public User User { get; set; } = null!;
    }
}
