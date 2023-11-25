using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class ExpenseRepository : IRepository<Expense>
    {
        private readonly PFAContext _db;

        public ExpenseRepository(PFAContext db) => _db = db;

        public async Task Create(Expense item) => await _db.Expense.AddAsync(item);

        public async Task<bool> Delete(int id)
        {
            var expense = await _db.Expense.FindAsync(id);
            if (expense == null)
                return false;

            _db.Expense.Remove(expense);
            return true;
        }

        public async Task<bool> Exists(int id) => await _db.Expense.AnyAsync(e => e.Id == id);

        public async Task<List<Expense>> GetAll() => await _db.Expense.ToListAsync();

        public async Task<Expense> GetItem(int id) => await _db.Expense.FindAsync(id);

        public async Task Update(Expense item) => _db.Entry(item).State = EntityState.Modified;
    }
}
