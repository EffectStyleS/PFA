using Microsoft.EntityFrameworkCore;
using PFA_Mobile_v2.Application.DatabaseInteraction.Interfaces;
using PFA_Mobile_v2.Domain.Entities;

namespace PFA_Mobile_v2.Infrastructure.Persistence.Repositories;

/// <summary>
/// Репозиторий запланированных расходов
/// </summary>
public class PlannedExpensesRepository : IRepository<PlannedExpenses>
{
    private readonly PfaContext _db;

    /// <summary>
    /// Репозиторий запланированных расходов
    /// </summary>
    public PlannedExpensesRepository(PfaContext db) => _db = db;

    /// <inheritdoc />
    public async Task Create(PlannedExpenses item) => await _db.PlannedExpenses.AddAsync(item);

    /// <inheritdoc />
    public async Task<bool> Delete(int id)
    {
        var plannedExpenses = await _db.PlannedExpenses.FindAsync(id);
        if (plannedExpenses == null)
            return false;

        _db.PlannedExpenses.Remove(plannedExpenses);
        return true;
    }

    /// <inheritdoc />
    public async Task<bool> Exists(int id) => await _db.PlannedExpenses.AnyAsync(pe => pe.Id == id);

    /// <inheritdoc />
    public async Task<List<PlannedExpenses>> GetAll() => await _db.PlannedExpenses.ToListAsync();

    /// <inheritdoc />
    public async Task<PlannedExpenses?> GetItem(int id) => await _db.PlannedExpenses.FindAsync(id);

    /// <inheritdoc />
    public async Task Update(PlannedExpenses item) => await Task.Run(() => _db.Entry(item).State = EntityState.Modified);
}