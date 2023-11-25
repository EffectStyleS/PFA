namespace DAL.Entities
{
    public class Goal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? Sum { get; set; }
        public bool IsCompleted { get; set; }
        public int UserId { get; set; }

        public virtual AppUser User { get; set; }
    }
}
