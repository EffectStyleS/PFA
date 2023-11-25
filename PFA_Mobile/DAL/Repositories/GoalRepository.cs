using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class GoalRepository : IRepository<Goal>
    {
        private readonly PFAContext _db;

        public GoalRepository(PFAContext db) => _db = db;

        public async Task Create(Goal item) => await _db.Goal.AddAsync(item);

        public async Task<bool> Delete(int id)
        {
            var goal = await _db.Goal.FindAsync(id);
            if (goal == null)
                return false;

            _db.Goal.Remove(goal);
            return true;
        }

        public async Task<bool> Exists(int id) => await _db.Goal.AnyAsync(g => g.Id == id);

        public async Task<List<Goal>> GetAll() => await _db.Goal.ToListAsync();

        public async Task<Goal> GetItem(int id) => await _db.Goal.FindAsync(id);

        public async Task Update(Goal item) => _db.Entry(item).State = EntityState.Modified;
    }
}
