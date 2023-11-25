using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class UserRepository : IUserRepository<AppUser>
    {
        private readonly PFAContext _db;

        public UserRepository(PFAContext db) => _db = db;

        public async Task<IdentityResult> Create(AppUser item, string password, UserManager<AppUser> userManager) 
            => await userManager.CreateAsync(item, password);

        public async Task<IdentityResult> Delete(string login, UserManager<AppUser> userManager)
        {
            var user = await userManager.FindByNameAsync(login);
            IdentityResult result = new IdentityResult();
            if (user != null)
            {
                result = await userManager.DeleteAsync(user);
            }
            return result;
        }

        public async Task<bool> Exists(string login, UserManager<AppUser> userManager)
        {
            var user = await userManager.FindByNameAsync(login);


            return user != null;
        }

        public async Task<List<AppUser>> GetAll(UserManager<AppUser> userManager)
        {
            return await userManager.Users.ToListAsync();
        }

        public async Task<AppUser> GetItem(string login, UserManager<AppUser> userManager)
        {
            var user = await userManager.FindByNameAsync(login);
            return user;
        }

        public async Task<IdentityResult> Update(AppUser item, UserManager<AppUser> userManager)
        {
            var result = await userManager.UpdateAsync(item);
            _db.Entry(item).State = EntityState.Modified;

            return result;
        }

        public async Task<List<IdentityRole<int>>> GetUserRoles(AppUser item)
        {
            var roleIds = await _db.UserRoles.Where(r => r.UserId == item.Id).Select(x => x.RoleId).ToListAsync();
            return await _db.Roles.Where(x => roleIds.Contains(x.Id)).ToListAsync();
        }
    }
}
