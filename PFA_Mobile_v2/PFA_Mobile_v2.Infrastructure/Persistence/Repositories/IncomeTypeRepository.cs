using Microsoft.EntityFrameworkCore;
using PFA_Mobile_v2.Application.DatabaseInteraction.Interfaces;
using PFA_Mobile_v2.Domain.Entities;

namespace PFA_Mobile_v2.Infrastructure.Persistence.Repositories;

/// <summary>
/// Репозиторий типа дохода
/// </summary>
public class IncomeTypeRepository : IRepository<IncomeType>
{
    private readonly PfaContext _db;

    /// <summary>
    /// Репозиторий типа дохода
    /// </summary>
    public IncomeTypeRepository(PfaContext db) => _db = db;

    /// <inheritdoc />
    public async Task Create(IncomeType item) => await _db.IncomeType.AddAsync(item);

    /// <inheritdoc />
    public async Task<bool> Delete(int id)
    {
        var incomeType = await _db.IncomeType.FindAsync(id);
        if (incomeType == null)
            return false;

        _db.IncomeType.Remove(incomeType);
        return true;
    }

    /// <inheritdoc />
    public async Task<bool> Exists(int id) => await _db.IncomeType.AnyAsync(it => it.Id == id);

    /// <inheritdoc />
    public async Task<List<IncomeType>> GetAll() => await _db.IncomeType.ToListAsync();

    /// <inheritdoc />
    public async Task<IncomeType?> GetItem(int id) => await _db.IncomeType.FindAsync(id);

    /// <inheritdoc />
    public async Task Update(IncomeType item) => await Task.Run(() => _db.Entry(item).State = EntityState.Modified);
}