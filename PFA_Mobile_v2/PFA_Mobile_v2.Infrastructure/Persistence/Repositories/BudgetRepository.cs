using Microsoft.EntityFrameworkCore;
using PFA_Mobile_v2.Application.DatabaseInteraction.Interfaces;
using PFA_Mobile_v2.Domain.Entities;

namespace PFA_Mobile_v2.Infrastructure.Persistence.Repositories;

/// <summary>
/// Репозиторий бюджета
/// </summary>
public class BudgetRepository : IRepository<Budget>
{
    private readonly PfaContext _db;

    /// <summary>
    /// Репозиторий бюджета
    /// </summary>
    public BudgetRepository(PfaContext db) => _db = db;
    
    /// <inheritdoc />
    public async Task Create(Budget item) => await _db.Budget.AddAsync(item);

    /// <inheritdoc />
    public async Task<bool> Delete(int id)
    {
        var budget = await _db.Budget.FindAsync(id);
        if (budget == null) 
            return false;

        _db.Budget.Remove(budget);
        return true;
    }

    /// <inheritdoc />
    public async Task<bool> Exists(int id) => await _db.Budget.AnyAsync(b => b.Id == id);

    /// <inheritdoc />
    public async Task<List<Budget>> GetAll() => await _db.Budget.ToListAsync();

    /// <inheritdoc />
    public async Task<Budget?> GetItem(int id) => await _db.Budget.FindAsync(id);

    /// <inheritdoc />
    public async Task Update(Budget item) => await Task.Run(() => _db.Entry(item).State = EntityState.Modified);
}
