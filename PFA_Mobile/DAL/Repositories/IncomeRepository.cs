using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class IncomeRepository : IRepository<Income>
    {
        private readonly PFAContext _db;

        public IncomeRepository(PFAContext db) => _db = db;

        public async Task Create(Income item) => await _db.Income.AddAsync(item);

        public async Task<bool> Delete(int id)
        {
            var income = await _db.Income.FindAsync(id);
            if (income == null)
                return false;

            _db.Income.Remove(income);
            return true;
        }

        public async Task<bool> Exists(int id) => await _db.Income.AnyAsync(i => i.Id == id);

        public async Task<List<Income>> GetAll() => await _db.Income.ToListAsync();

        public async Task<Income> GetItem(int id) => await _db.Income.FindAsync(id);

        public async Task Update(Income item) => _db.Entry(item).State = EntityState.Modified;
    }
}
