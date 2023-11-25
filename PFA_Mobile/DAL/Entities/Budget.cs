namespace DAL.Entities
{
    public class Budget
    {
        public Budget() 
        {
            PlannedExpenses = new HashSet<PlannedExpenses>();
            PlannedIncomes  = new HashSet<PlannedIncomes>();
        }

        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public int TimePeriodId { get; set; }
        public int UserId { get; set; }

        public virtual TimePeriod TimePeriod { get; set; }
        public virtual AppUser User { get; set; }

        public virtual ICollection<PlannedExpenses> PlannedExpenses { get; set; }
        public virtual ICollection<PlannedIncomes> PlannedIncomes { get; set; }
    }
}
