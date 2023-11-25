namespace client.Model.Models
{
    public class PlannedExpensesModel : BaseModel
    {
        public decimal? Sum { get; set; }
        public int ExpenseTypeId { get; set; }
        public int BudgetId { get; set; }

        public string ExpenseType { get; set; }
    }
}
