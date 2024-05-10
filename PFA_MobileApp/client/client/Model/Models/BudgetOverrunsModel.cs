namespace client.Model.Models;

// вспомогательная модель для отображения превышений доходов
public class BudgetOverrunsModel
{
    public string BudgetName { get; set; }
    public string ExpenseType { get; set; }
    public decimal Difference { get; set; }
}