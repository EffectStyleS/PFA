using Microsoft.EntityFrameworkCore;
using PFA_Mobile_v2.Application.DatabaseInteraction.Interfaces;
using PFA_Mobile_v2.Domain.Entities;

namespace PFA_Mobile_v2.Infrastructure.Persistence.Repositories;

/// <summary>
/// Репозиторий дохода
/// </summary>
public class IncomeRepository : IRepository<Income>
{
    private readonly PfaContext _db;

    /// <summary>
    /// Репозиторий дохода
    /// </summary>
    public IncomeRepository(PfaContext db) => _db = db;

    /// <inheritdoc />
    public async Task Create(Income item) => await _db.Income.AddAsync(item);

    /// <inheritdoc />
    public async Task<bool> Delete(int id)
    {
        var income = await _db.Income.FindAsync(id);
        if (income == null)
            return false;

        _db.Income.Remove(income);
        return true;
    }

    /// <inheritdoc />
    public async Task<bool> Exists(int id) => await _db.Income.AnyAsync(i => i.Id == id);

    /// <inheritdoc />
    public async Task<List<Income>> GetAll() => await _db.Income.ToListAsync();

    /// <inheritdoc />
    public async Task<Income?> GetItem(int id) => await _db.Income.FindAsync(id);

    /// <inheritdoc />
    public async Task Update(Income item) => await Task.Run(() => _db.Entry(item).State = EntityState.Modified);
}