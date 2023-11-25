using DAL.Entities;

namespace BLL.DTOs
{
    public class PlannedExpensesDTO
    {
        public PlannedExpensesDTO() { }
        public PlannedExpensesDTO(PlannedExpenses plannedExpenses)
        {
            Id             = plannedExpenses.Id;
            Sum            = plannedExpenses.Sum;
            ExpenseTypeId  = plannedExpenses.ExpenseTypeId;
            BudgetId       = plannedExpenses.BudgetId;
        }

        public int Id { get; set; }
        public decimal? Sum { get; set; }
        public int ExpenseTypeId { get; set; }
        public int BudgetId { get; set; }
    }
}
