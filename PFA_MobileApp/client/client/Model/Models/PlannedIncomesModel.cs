namespace client.Model.Models
{
    public class PlannedIncomesModel : BaseModel
    {
        public decimal? Sum { get; set; }
        public int IncomeTypeId { get; set; }
        public int BudgetId { get; set; }
         
        public string IncomeType { get; set; }
    }
}
