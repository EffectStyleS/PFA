using DAL.Entities;

namespace BLL.DTOs
{
    public class GoalDTO
    {
        public GoalDTO() { }

        public GoalDTO(Goal goal) 
        {
            Id          = goal.Id;
            Name        = goal.Name;
            StartDate   = goal.StartDate;
            EndDate     = goal.EndDate;
            Sum         = goal.Sum;
            IsCompleted = goal.IsCompleted;
            UserId      = goal.UserId;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? Sum { get; set; }
        public bool IsCompleted { get; set; }
        public int UserId { get; set; }
    }
}
