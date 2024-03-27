using Microsoft.EntityFrameworkCore;
using PFA_Mobile_v2.Application.DatabaseInteraction.Interfaces;
using PFA_Mobile_v2.Domain.Entities;

namespace PFA_Mobile_v2.Infrastructure.Persistence.Repositories;

/// <summary>
/// Репозиторий запланированных доходов
/// </summary>
public class PlannedIncomesRepository : IRepository<PlannedIncomes>
{
    private readonly PfaContext _db;

    /// <summary>
    /// Репозиторий запланированных доходов
    /// </summary>
    public PlannedIncomesRepository(PfaContext db) => _db = db;

    /// <inheritdoc />
    public async Task Create(PlannedIncomes item) => await _db.PlannedIncomes.AddAsync(item);

    /// <inheritdoc />
    public async Task<bool> Delete(int id)
    {
        var plannedIncomes = await _db.PlannedIncomes.FindAsync(id);
        if (plannedIncomes == null)
            return false;

        _db.PlannedIncomes.Remove(plannedIncomes);
        return true;
    }

    /// <inheritdoc />
    public async Task<bool> Exists(int id) => await _db.PlannedIncomes.AnyAsync(pi => pi.Id == id);

    /// <inheritdoc />
    public async Task<List<PlannedIncomes>> GetAll() => await _db.PlannedIncomes.ToListAsync();

    /// <inheritdoc />
    public async Task<PlannedIncomes?> GetItem(int id) => await _db.PlannedIncomes.FindAsync(id);

    /// <inheritdoc />
    public async Task Update(PlannedIncomes item) => await Task.Run(() => _db.Entry(item).State = EntityState.Modified);
}