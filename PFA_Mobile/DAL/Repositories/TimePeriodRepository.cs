using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class TimePeriodRepository : IRepository<TimePeriod>
    {
        private readonly PFAContext _db;

        public TimePeriodRepository(PFAContext db) => _db = db;

        public async Task Create(TimePeriod item) => await _db.TimePeriod.AddAsync(item);

        public async Task<bool> Delete(int id)
        {
            var timePeriod = await _db.TimePeriod.FindAsync(id);
            if (timePeriod == null)
                return false;

            _db.TimePeriod.Remove(timePeriod);
            return true;
        }

        public async Task<bool> Exists(int id) => await _db.TimePeriod.AnyAsync(tp => tp.Id == id);

        public async Task<List<TimePeriod>> GetAll() => await _db.TimePeriod.ToListAsync();

        public async Task<TimePeriod> GetItem(int id) => await _db.TimePeriod.FindAsync(id);

        public async Task Update(TimePeriod item) => _db.Entry(item).State = EntityState.Modified;
    }
}
