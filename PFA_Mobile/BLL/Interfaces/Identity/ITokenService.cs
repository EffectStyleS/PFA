using BLL.DTOs;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace BLL.Interfaces.Identity
{
    public interface ITokenService
    {
        Task<string> CreateToken(UserDTO userDTO, UserManager<AppUser> userManager, List<IdentityRole<int>> role);
    }
}
