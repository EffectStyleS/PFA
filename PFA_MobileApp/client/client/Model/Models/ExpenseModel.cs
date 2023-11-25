namespace client.Model.Models
{
    public class ExpenseModel : BaseModel
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
        public int ExpenseTypeId { get; set; }
        public string ExpenseType { get; set; } // для отображения
        public int UserId { get; set; }
    }
}
