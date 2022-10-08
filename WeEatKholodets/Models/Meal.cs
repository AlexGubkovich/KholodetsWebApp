namespace WeEatKholodets.Models
{
    public class Meal
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        //public string UserId = null!;
        public User User { get; set; } = null!;
    }
}
