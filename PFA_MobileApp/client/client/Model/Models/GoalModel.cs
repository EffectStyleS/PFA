namespace client.Model.Models
{
    public class GoalModel : BaseModel
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Sum { get; set; }
        public bool IsCompleted { get; set; }
        public int UserId { get; set; }

    }
}
