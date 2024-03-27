using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PFA_Mobile_v2.Domain.Entities;

namespace PFA_Mobile_v2.Infrastructure.Persistence;

/// <summary>
/// Контекст подключения
/// </summary>
public partial class PfaContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
{
    protected readonly IConfiguration Configuration;

    /// <summary>
    /// Контекст подключения
    /// </summary>
    public PfaContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        try
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }
        catch
        {
            throw new Exception("Db server isn't working");
        }
    }

    public virtual DbSet<Budget> Budget { get; set; }
    public virtual DbSet<Expense> Expense { get; set; }
    public virtual DbSet<ExpenseType> ExpenseType { get; set; }
    public virtual DbSet<Income> Income { get; set; }
    public virtual DbSet<IncomeType> IncomeType { get; set; }
    public virtual DbSet<PlannedExpenses> PlannedExpenses { get; set; }
    public virtual DbSet<PlannedIncomes> PlannedIncomes { get; set; }
    public virtual DbSet<TimePeriod> TimePeriod { get; set; }
    public virtual DbSet<AppUser> User { get; set; }
    public virtual DbSet<Goal> Goal { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Budget>(entity =>
        {
            entity.HasOne(b => b.User)
                  .WithMany(u => u.Budgets)
                  .HasForeignKey(b => b.UserId);

            entity.HasOne(b => b.TimePeriod)
                  .WithMany(t => t.Budgets)
                  .HasForeignKey(b => b.TimePeriodId);
        });

        modelBuilder.Entity<Income>(entity =>
        {
            entity.HasOne(i => i.User)
                  .WithMany(u => u.Incomes)
                  .HasForeignKey(i => i.UserId);

            entity.HasOne(i => i.IncomeType)
                  .WithMany(it => it.Incomes)
                  .HasForeignKey(i => i.IncomeTypeId);
        });

        modelBuilder.Entity<Expense>(entity =>
        {
            entity.HasOne(e => e.User)
                  .WithMany(u => u.Expenses)
                  .HasForeignKey(e => e.UserId);

            entity.HasOne(e => e.ExpenseType)
                  .WithMany(et => et.Expenses)
                  .HasForeignKey(e => e.ExpenseTypeId);
        });

        modelBuilder.Entity<PlannedExpenses>(entity =>
        {
            entity.HasOne(pe => pe.Budget)
                  .WithMany(b => b.PlannedExpenses)
                  .HasForeignKey(pe => pe.BudgetId);

            entity.HasOne(pe => pe.ExpenseType)
                  .WithMany(et => et.PlannedExpenses)
                  .HasForeignKey(pe => pe.ExpenseTypeId);
        });

        modelBuilder.Entity<PlannedIncomes>(entity =>
        {
            entity.HasOne(pi => pi.Budget)
                  .WithMany(b => b.PlannedIncomes)
                  .HasForeignKey(pi => pi.BudgetId);

            entity.HasOne(pi => pi.IncomeType)
                  .WithMany(it => it.PlannedIncomes)
                  .HasForeignKey(pi => pi.IncomeTypeId);
        });

        modelBuilder.Entity<Goal>(entity =>
        {
            entity.HasOne(b => b.User)
                  .WithMany(u => u.Goals)
                  .HasForeignKey(b => b.UserId);
        });
    }
}