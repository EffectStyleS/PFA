namespace DAL.Entities
{
    public class Income
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
        public int IncomeTypeId { get; set; }
        public int UserId { get; set; }

        public virtual IncomeType IncomeType { get; set; }
        public virtual AppUser User { get; set; }
    }
}
