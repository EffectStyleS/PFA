using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class BudgetRepository : IRepository<Budget>
    {
        private readonly PFAContext _db;

        public BudgetRepository(PFAContext db) => _db = db;

        public async Task Create(Budget item) => await _db.Budget.AddAsync(item);

        public async Task<bool> Delete(int id)
        {
            var budget = await _db.Budget.FindAsync(id);
            if (budget == null) 
                return false;

            _db.Budget.Remove(budget);
            return true;
        }

        public async Task<bool> Exists(int id) => await _db.Budget.AnyAsync(b => b.Id == id);

        public async Task<List<Budget>> GetAll() => await _db.Budget.ToListAsync();

        public async Task<Budget> GetItem(int id) => await _db.Budget.FindAsync(id);

        public async Task Update(Budget item) => _db.Entry(item).State = EntityState.Modified;
    }
}
