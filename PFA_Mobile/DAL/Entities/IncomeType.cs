namespace DAL.Entities
{
    public class IncomeType
    {
        public IncomeType()
        {
            Incomes = new HashSet<Income>();
            PlannedIncomes = new HashSet<PlannedIncomes>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Income> Incomes { get; set; }
        public virtual ICollection<PlannedIncomes> PlannedIncomes { get; set; }
    }
}
