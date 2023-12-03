using DAL.Entities;

namespace BLL.DTOs
{
    public class BudgetDTO
    {
        public BudgetDTO() { }

        public BudgetDTO(Budget budget)
        {
            Id              = budget.Id;
            Name            = budget.Name;
            StartDate       = budget.StartDate;
            TimePeriodId    = budget.TimePeriodId;
            UserId          = budget.UserId;
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime StartDate { get; set; }
        public int TimePeriodId { get; set; }
        public int UserId { get; set; }
    }
}

