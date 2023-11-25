using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class ExpenseTypeRepository : IRepository<ExpenseType>
    {
        private readonly PFAContext _db;

        public ExpenseTypeRepository(PFAContext db) => _db = db;

        public async Task Create(ExpenseType item) => await _db.ExpenseType.AddAsync(item);

        public async Task<bool> Delete(int id)
        {
            var expenseType = await _db.ExpenseType.FindAsync(id);
            if (expenseType == null)
                return false;

            _db.ExpenseType.Remove(expenseType);
            return true;
        }

        public async Task<bool> Exists(int id) => await _db.ExpenseType.AnyAsync(et => et.Id == id);

        public async Task<List<ExpenseType>> GetAll() => await _db.ExpenseType.ToListAsync();

        public async Task<ExpenseType> GetItem(int id) => await _db.ExpenseType.FindAsync(id);

        public async Task Update(ExpenseType item) => _db.Entry(item).State = EntityState.Modified;
    }
}
