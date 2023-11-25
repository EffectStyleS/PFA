namespace client.Model.Models
{
    public class BudgetModel : BaseModel
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public int TimePeriodId { get; set; }
        public int UserId { get; set; }

        public decimal? Saldo { get; set; }
        public string TimePeriod {  get; set; }
        public List<PlannedIncomesModel> PlannedIncomes { get; set; }
        public List<PlannedExpensesModel> PlannedExpenses { get; set; }
    }
}
