using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class PlannedExpensesRepository : IRepository<PlannedExpenses>
    {
        private readonly PFAContext _db;

        public PlannedExpensesRepository(PFAContext db) => _db = db;

        public async Task Create(PlannedExpenses item) => await _db.PlannedExpenses.AddAsync(item);

        public async Task<bool> Delete(int id)
        {
            var plannedExpenses = await _db.PlannedExpenses.FindAsync(id);
            if (plannedExpenses == null)
                return false;

            _db.PlannedExpenses.Remove(plannedExpenses);
            return true;
        }

        public async Task<bool> Exists(int id) => await _db.PlannedExpenses.AnyAsync(pe => pe.Id == id);

        public async Task<List<PlannedExpenses>> GetAll() => await _db.PlannedExpenses.ToListAsync();

        public async Task<PlannedExpenses> GetItem(int id) => await _db.PlannedExpenses.FindAsync(id);

        public async Task Update(PlannedExpenses item) => _db.Entry(item).State = EntityState.Modified;
    }
}
