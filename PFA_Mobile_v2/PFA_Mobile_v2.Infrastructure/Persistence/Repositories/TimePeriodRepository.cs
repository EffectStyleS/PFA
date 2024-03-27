using Microsoft.EntityFrameworkCore;
using PFA_Mobile_v2.Application.DatabaseInteraction.Interfaces;
using PFA_Mobile_v2.Domain.Entities;

namespace PFA_Mobile_v2.Infrastructure.Persistence.Repositories;

/// <summary>
/// Репозиторий временных периодов
/// </summary>
public class TimePeriodRepository : IRepository<TimePeriod>
{
    private readonly PfaContext _db;
    
    /// <summary>
    /// Репозиторий временных периодов
    /// </summary>
    public TimePeriodRepository(PfaContext db) => _db = db;

    /// <inheritdoc />
    public async Task Create(TimePeriod item) => await _db.TimePeriod.AddAsync(item);

    /// <inheritdoc />
    public async Task<bool> Delete(int id)
    {
        var timePeriod = await _db.TimePeriod.FindAsync(id);
        if (timePeriod == null)
            return false;

        _db.TimePeriod.Remove(timePeriod);
        return true;
    }

    /// <inheritdoc />
    public async Task<bool> Exists(int id) => await _db.TimePeriod.AnyAsync(tp => tp.Id == id);

    /// <inheritdoc />
    public async Task<List<TimePeriod>> GetAll() => await _db.TimePeriod.ToListAsync();

    /// <inheritdoc />
    public async Task<TimePeriod?> GetItem(int id) => await _db.TimePeriod.FindAsync(id);

    /// <inheritdoc />
    public async Task Update(TimePeriod item) => await Task.Run(() => _db.Entry(item).State = EntityState.Modified);
}