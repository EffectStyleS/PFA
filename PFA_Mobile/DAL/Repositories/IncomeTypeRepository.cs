using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class IncomeTypeRepository : IRepository<IncomeType>
    {
        private readonly PFAContext _db;

        public IncomeTypeRepository(PFAContext db) => _db = db;

        public async Task Create(IncomeType item) => await _db.IncomeType.AddAsync(item);

        public async Task<bool> Delete(int id)
        {
            var incomeType = await _db.IncomeType.FindAsync(id);
            if (incomeType == null)
                return false;

            _db.IncomeType.Remove(incomeType);
            return true;
        }

        public async Task<bool> Exists(int id) => await _db.IncomeType.AnyAsync(it => it.Id == id);

        public async Task<List<IncomeType>> GetAll() => await _db.IncomeType.ToListAsync();

        public async Task<IncomeType> GetItem(int id) => await _db.IncomeType.FindAsync(id);

        public async Task Update(IncomeType item) => _db.Entry(item).State = EntityState.Modified;
    }
}
