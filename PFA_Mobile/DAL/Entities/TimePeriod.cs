namespace DAL.Entities
{
    public class TimePeriod
    {
        public TimePeriod() 
        {
            Budgets = new HashSet<Budget>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Budget> Budgets { get; set; }
    }
}
