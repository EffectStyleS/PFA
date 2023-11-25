using DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs
{
    public class PlannedIncomesDTO
    {
        public PlannedIncomesDTO() { }

        public PlannedIncomesDTO(PlannedIncomes plannedIncomes)
        {
            Id           = plannedIncomes.Id;
            Sum          = plannedIncomes.Sum;
            IncomeTypeId = plannedIncomes.IncomeTypeId;
            BudgetId     = plannedIncomes.BudgetId;
        }

        public int Id { get; set; }
        public decimal? Sum { get; set; }
        public int IncomeTypeId { get; set; }
        public int BudgetId { get; set; }
    }
}
