using Microsoft.EntityFrameworkCore;
using PFA_Mobile_v2.Application.DatabaseInteraction.Interfaces;
using PFA_Mobile_v2.Domain.Entities;

namespace PFA_Mobile_v2.Infrastructure.Persistence.Repositories;

/// <summary>
/// Репозиторий типа расхода
/// </summary>
public class ExpenseTypeRepository : IRepository<ExpenseType>
{
    private readonly PfaContext _db;

    /// <summary>
    /// Репозиторий типа расхода
    /// </summary>
    public ExpenseTypeRepository(PfaContext db) => _db = db;

    /// <inheritdoc />
    public async Task Create(ExpenseType item) => await _db.ExpenseType.AddAsync(item);

    /// <inheritdoc />
    public async Task<bool> Delete(int id)
    {
        var expenseType = await _db.ExpenseType.FindAsync(id);
        if (expenseType == null)
            return false;

        _db.ExpenseType.Remove(expenseType);
        return true;
    }

    /// <inheritdoc />
    public async Task<bool> Exists(int id) => await _db.ExpenseType.AnyAsync(et => et.Id == id);

    /// <inheritdoc />
    public async Task<List<ExpenseType>> GetAll() => await _db.ExpenseType.ToListAsync();

    /// <inheritdoc />
    public async Task<ExpenseType?> GetItem(int id) => await _db.ExpenseType.FindAsync(id);

    /// <inheritdoc />
    public async Task Update(ExpenseType item) => await Task.Run(() => _db.Entry(item).State = EntityState.Modified);
}