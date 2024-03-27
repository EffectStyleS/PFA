using Microsoft.EntityFrameworkCore;
using PFA_Mobile_v2.Application.DatabaseInteraction.Interfaces;
using PFA_Mobile_v2.Domain.Entities;

namespace PFA_Mobile_v2.Infrastructure.Persistence.Repositories;

/// <summary>
/// Репозиторий цели
/// </summary>
public class GoalRepository : IRepository<Goal>
{
    private readonly PfaContext _db;

    /// <summary>
    /// Репозиторий цели
    /// </summary>
    public GoalRepository(PfaContext db) => _db = db;

    /// <inheritdoc />
    public async Task Create(Goal item) => await _db.Goal.AddAsync(item);

    /// <inheritdoc />
    public async Task<bool> Delete(int id)
    {
        var goal = await _db.Goal.FindAsync(id);
        if (goal == null)
            return false;

        _db.Goal.Remove(goal);
        return true;
    }

    /// <inheritdoc />
    public async Task<bool> Exists(int id) => await _db.Goal.AnyAsync(g => g.Id == id);

    /// <inheritdoc />
    public async Task<List<Goal>> GetAll() => await _db.Goal.ToListAsync();

    /// <inheritdoc />
    public async Task<Goal?> GetItem(int id) => await _db.Goal.FindAsync(id);

    /// <inheritdoc />
    public async Task Update(Goal item) => await Task.Run(() => _db.Entry(item).State = EntityState.Modified);
}