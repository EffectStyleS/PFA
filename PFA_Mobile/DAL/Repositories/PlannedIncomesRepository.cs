using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class PlannedIncomesRepository : IRepository<PlannedIncomes>
    {
        private readonly PFAContext _db;

        public PlannedIncomesRepository(PFAContext db) => _db = db;

        public async Task Create(PlannedIncomes item) => await _db.PlannedIncomes.AddAsync(item);

        public async Task<bool> Delete(int id)
        {
            var plannedIncomes = await _db.PlannedIncomes.FindAsync(id);
            if (plannedIncomes == null)
                return false;

            _db.PlannedIncomes.Remove(plannedIncomes);
            return true;
        }

        public async Task<bool> Exists(int id) => await _db.PlannedIncomes.AnyAsync(pi => pi.Id == id);

        public async Task<List<PlannedIncomes>> GetAll() => await _db.PlannedIncomes.ToListAsync();

        public async Task<PlannedIncomes> GetItem(int id) => await _db.PlannedIncomes.FindAsync(id);

        public async Task Update(PlannedIncomes item) => _db.Entry(item).State = EntityState.Modified;
    }
}
