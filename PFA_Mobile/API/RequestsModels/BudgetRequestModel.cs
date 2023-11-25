using BLL.DTOs;

namespace API.RequestsModels
{
    public class BudgetRequestModel
    {
        public BudgetDTO Budget { get; set; }
        
        public List<PlannedExpensesDTO> PlannedExpenses { get; set; }

        public List<PlannedIncomesDTO> PlannedIncomes { get; set;  }
    }
}
