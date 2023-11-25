namespace client.Model.Models
{
    public class IncomeModel : BaseModel
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
        public int IncomeTypeId { get; set; }
        public string IncomeType { get; set; } // для отображения
        public int UserId { get; set; }
    }
}
