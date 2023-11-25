namespace DAL.Entities
{
    public class ExpenseType
    {
        public ExpenseType()
        {
            Expenses = new HashSet<Expense>();
            PlannedExpenses = new HashSet<PlannedExpenses>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Expense> Expenses { get; set; }
        public virtual ICollection<PlannedExpenses> PlannedExpenses { get; set; }
    }
}
