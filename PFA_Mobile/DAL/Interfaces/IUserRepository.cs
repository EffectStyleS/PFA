using DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace DAL.Interfaces
{
    public interface IUserRepository<T> where T : IdentityUser<int>
    {
        Task<List<T>> GetAll(UserManager<AppUser> userManager);

        Task<T> GetItem(string login, UserManager<T> userManager);
        Task<IdentityResult> Create(T item, string password, UserManager<T> userManager);
        Task<IdentityResult> Update(T item, UserManager<T> userManager);
        Task<IdentityResult> Delete(string login, UserManager<T> userManager);
        Task<bool> Exists(string login, UserManager<T> userManager);

        Task<List<IdentityRole<int>>> GetUserRoles(AppUser item);
    }
}
