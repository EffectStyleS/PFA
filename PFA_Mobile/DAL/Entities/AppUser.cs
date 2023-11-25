using Microsoft.AspNetCore.Identity;

namespace DAL.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public AppUser()
        {
            Expenses = new HashSet<Expense>();
            Incomes = new HashSet<Income>();
            Budgets = new HashSet<Budget>();
            Goals = new HashSet<Goal>();
        }

        public string Login { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

        public virtual ICollection<Expense> Expenses { get; set; }
        public virtual ICollection<Income> Incomes { get; set; }
        public virtual ICollection<Budget> Budgets { get; set; }
        public virtual ICollection<Goal> Goals { get; set; }
    }
}
