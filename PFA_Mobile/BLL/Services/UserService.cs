using BLL.DTOs;
using BLL.Infrastructure.Extensions;
using BLL.Interfaces;
using BLL.Services.Abstract;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BLL.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task AddToRoleAsync(UserDTO userDTO, string roleName, UserManager<AppUser> userManager)
        {
            var user = await userManager.FindByNameAsync(userDTO.Login);
            await userManager.AddToRoleAsync(user, roleName);
        }

        public async Task<bool> CheckPasswordAsync(UserDTO userDTO, string password, UserManager<AppUser> userManager)
        {
            var user = await userManager.FindByNameAsync(userDTO.Login);
            return await userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IdentityResult> CreateAsync(UserDTO userDTO, string password, UserManager<AppUser> userManager)
        {
            var newUser = new AppUser()
            {
                Login = userDTO.Login,
                UserName = userDTO.Login, // для нахождения пользователя по имени
                RefreshToken = userDTO.RefreshToken,
                RefreshTokenExpiryTime = userDTO.RefreshTokenExpiryTime,
            };

            var result = await _unitOfWork.User.Create(newUser, password, userManager);

            if (result.Succeeded) 
            {
                await SaveAsync(); 
            }

            return result;
        }

        public async Task<bool> CreateRefreshToken(UserDTO userDTO, UserManager<AppUser> userManager, IConfiguration configuration)
        {
            userDTO.RefreshToken = configuration.GenerateRefreshToken();
            
            var refreshTokenValidityInDays = configuration.GetSection("Jwt:RefreshTokenValidityInDays").Value;
            userDTO.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(int.Parse(refreshTokenValidityInDays));

            var user = await userManager.FindByNameAsync(userDTO.Login);
            user.RefreshToken = userDTO.RefreshToken;
            user.RefreshTokenExpiryTime = userDTO.RefreshTokenExpiryTime;

            var result = await _unitOfWork.User.Update(user, userManager);

            return result.Succeeded && await SaveAsync();
        }

        public async Task<List<UserDTO>> GetAllAsync(UserManager<AppUser> userManager)
        {
            var users = await _unitOfWork.User.GetAll(userManager);

            var result = users
                .Select(u => new UserDTO(u))
                .ToList();

            return result;
        }

        public async Task<UserDTO> GetByLoginAsync(string login, UserManager<AppUser> userManager)
        {
            var user = await _unitOfWork.User.GetItem(login, userManager);

            return user == null ? null : new UserDTO(user);
        }
            

        public async Task<List<IdentityRole<int>>> GetUserRoles(UserDTO userDTO, UserManager<AppUser> userManager)
        {
            var user = await userManager.FindByNameAsync(userDTO.Login);
            return await _unitOfWork.User.GetUserRoles(user);
        }

        public async Task<bool> ResetAllRefreshTokens(UserManager<AppUser> userManager)
        {
            var userList = await userManager.Users.ToListAsync();

            foreach (var user in userList)
            {
                user.RefreshToken = null;
                var userDTO = await GetByLoginAsync(user.Login, userManager);
                userDTO.RefreshToken = null;

                var result = await userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    return false;
                }
            }

            return await SaveAsync();
        }

        public async Task<bool> ResetRefreshToken(UserDTO userDTO, UserManager<AppUser> userManager)
        {
            userDTO.RefreshToken = null;

            var user = await userManager.FindByNameAsync(userDTO.Login);
            user.RefreshToken = null;

            var result = await _unitOfWork.User.Update(user, userManager);

            return result.Succeeded && await SaveAsync();
        }
    }
}
