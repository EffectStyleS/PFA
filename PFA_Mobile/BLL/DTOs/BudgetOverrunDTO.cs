namespace BLL.DTOs
{
    public class BudgetOverrunDTO
    {
        public string? BudgetName { get; set; }
        public string? ExpenseType { get; set; }
        public decimal? Difference { get; set; }
    }
}
