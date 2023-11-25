using BLL.DTOs;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> GetByLoginAsync(string login, UserManager<AppUser> userManager);
        Task<bool> CheckPasswordAsync(UserDTO userDTO, string password, UserManager<AppUser> userManager);
        Task<List<UserDTO>> GetAllAsync(UserManager<AppUser> userManager);
        Task<IdentityResult> CreateAsync(UserDTO userDTO, string password, UserManager<AppUser> userManager);       
        Task AddToRoleAsync(UserDTO userDTO, string roleName, UserManager<AppUser> userManager);
        Task<List<IdentityRole<int>>> GetUserRoles(UserDTO userDTO, UserManager<AppUser> userManager);
        Task<bool> CreateRefreshToken(UserDTO userDTO, UserManager<AppUser> userManager, IConfiguration configuration);
        Task<bool> ResetRefreshToken(UserDTO userDTO, UserManager<AppUser> userManager);
        Task<bool> ResetAllRefreshTokens(UserManager<AppUser> userManager);
    }
}
