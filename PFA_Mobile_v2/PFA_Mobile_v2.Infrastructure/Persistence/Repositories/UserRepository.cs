using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PFA_Mobile_v2.Application.DatabaseInteraction.Interfaces;
using PFA_Mobile_v2.Domain.Entities;

namespace PFA_Mobile_v2.Infrastructure.Persistence.Repositories;

/// <summary>
/// Репозиторий пользователя
/// </summary>
public class UserRepository : IUserRepository<AppUser>
{
    private readonly PfaContext _db;

    /// <summary>
    /// Репозиторий пользователя
    /// </summary>
    public UserRepository(PfaContext db) => _db = db;

    /// <inheritdoc />
    public async Task<IdentityResult> Create(AppUser item, string password, UserManager<AppUser> userManager) 
        => await userManager.CreateAsync(item, password);

    /// <inheritdoc />
    public async Task<IdentityResult> Delete(string login, UserManager<AppUser> userManager)
    {
        var user = await userManager.FindByNameAsync(login);
        var result = new IdentityResult();
        if (user != null)
        {
            result = await userManager.DeleteAsync(user);
        }
        return result;
    }

    /// <inheritdoc />
    public async Task<bool> Exists(string login, UserManager<AppUser> userManager)
    {
        var user = await userManager.FindByNameAsync(login);
        return user != null;
    }

    /// <inheritdoc />
    public async Task<List<AppUser>> GetAll(UserManager<AppUser> userManager)
    {
        return await userManager.Users.ToListAsync();
    }

    /// <inheritdoc />
    public async Task<AppUser?> GetItem(string login, UserManager<AppUser> userManager)
    {
        var user = await userManager.FindByNameAsync(login);
        return user;
    }

    /// <inheritdoc />
    public async Task<IdentityResult> Update(AppUser item, UserManager<AppUser> userManager)
    {
        var result = await userManager.UpdateAsync(item);
        _db.Entry(item).State = EntityState.Modified;

        return result;
    }

    /// <inheritdoc />
    public async Task<List<IdentityRole<int>>> GetUserRoles(AppUser item)
    {
        var roleIds = await _db.UserRoles.Where(r => r.UserId == item.Id).Select(x => x.RoleId).ToListAsync();
        return await _db.Roles.Where(x => roleIds.Contains(x.Id)).ToListAsync();
    }
}