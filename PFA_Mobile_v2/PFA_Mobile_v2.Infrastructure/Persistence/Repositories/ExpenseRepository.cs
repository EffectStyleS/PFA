using Microsoft.EntityFrameworkCore;
using PFA_Mobile_v2.Application.DatabaseInteraction.Interfaces;
using PFA_Mobile_v2.Domain.Entities;

namespace PFA_Mobile_v2.Infrastructure.Persistence.Repositories;

/// <summary>
/// Репозиторий дохода
/// </summary>
public class ExpenseRepository : IRepository<Expense>
{
    private readonly PfaContext _db;

    /// <summary>
    /// Репозиторий дохода
    /// </summary>
    public ExpenseRepository(PfaContext db) => _db = db;

    /// <inheritdoc />
    public async Task Create(Expense item) => await _db.Expense.AddAsync(item);

    /// <inheritdoc />
    public async Task<bool> Delete(int id)
    {
        var expense = await _db.Expense.FindAsync(id);
        if (expense == null)
            return false;

        _db.Expense.Remove(expense);
        return true;
    }

    /// <inheritdoc />
    public async Task<bool> Exists(int id) => await _db.Expense.AnyAsync(e => e.Id == id);

    /// <inheritdoc />
    public async Task<List<Expense>> GetAll() => await _db.Expense.ToListAsync();

    /// <inheritdoc />
    public async Task<Expense?> GetItem(int id) => await _db.Expense.FindAsync(id);

    /// <inheritdoc />
    public async Task Update(Expense item) => await Task.Run(() => _db.Entry(item).State = EntityState.Modified);
}