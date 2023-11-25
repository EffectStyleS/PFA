using API.RequestsModels.Identity;
using BLL.DTOs;
using BLL.Interfaces;
using BLL.Interfaces.Identity;
using BLL.Infrastructure.Extensions;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<AppUser> userManager, IUserService userService, IConfiguration configuration, ITokenService tokenService)
        {
            _userManager = userManager;
            _userService = userService;
            _tokenService = tokenService;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Authenticate([FromBody] AuthRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var managedUser = await _userService.GetByLoginAsync(request.Login, _userManager);

            if (managedUser == null)
            {
                return BadRequest("Bad credentials");
            }

            var isPasswordValid = await _userService.CheckPasswordAsync(managedUser, request.Password, _userManager);
            
            if (!isPasswordValid)
            {
                return BadRequest("Bad credentials");
            }

            var user = await _userService.GetByLoginAsync(managedUser.Login, _userManager);

            if (user is null)
                return Unauthorized();

            var roles = await _userService.GetUserRoles(user, _userManager);

            var accessToken = await _tokenService.CreateToken(user, _userManager, roles);

            if (!await _userService.CreateRefreshToken(user, _userManager, _configuration))
            {
                return BadRequest("Smth gone wrong while creating tokens");
            }

            return Ok(new AuthResponse
            {
                Login = user.Login!,
                Token = accessToken,
                RefreshToken = user.RefreshToken
            });
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponse>> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(request);
            }

            var userModel = new UserDTO
            {
                Login = request.Login,
            };

            var result = await _userService.CreateAsync(userModel, request.Password, _userManager);

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            if (!result.Succeeded) return BadRequest(request);

            var findUser = await _userService.GetByLoginAsync(request.Login, _userManager) ?? throw new Exception($"User {request.Login} not found");

            await _userService.AddToRoleAsync(findUser, "Member", _userManager);

            return await Authenticate(new AuthRequest
            {
                Login = request.Login,
                Password = request.Password
            });
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenModel? tokenModel)
        {
            if (tokenModel is null)
            {
                return BadRequest("Invalid client request");
            }

            var accessToken = tokenModel.AccessToken;
            var refreshToken = tokenModel.RefreshToken;
            var principal = _configuration.GetPrincipalFromExpiredToken(accessToken);

            var login = principal.Identity!.Name;
            var user = await _userService.GetByLoginAsync(login!, _userManager);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return BadRequest("Invalid access token or refresh token");
            }

            var newAccessToken = _configuration.CreateToken(principal.Claims.ToList());
            var newRefreshToken = _userService.CreateRefreshToken(user, _userManager, _configuration);

            return new ObjectResult(new
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                refreshToken = newRefreshToken
            });
        }

        [Authorize]
        [HttpPost]
        [Route("revoke/{login}")]
        public async Task<IActionResult> Revoke(string login)
        {
            var user = await _userService.GetByLoginAsync(login, _userManager);

            if (user == null) 
                return BadRequest("Invalid user login");

            if (!await _userService.ResetRefreshToken(user, _userManager))
            {
                return BadRequest("Smth gone wrong while reseting refresh token");
            }

            return Ok();
        }

        [Authorize]
        [HttpPost]
        [Route("revoke-all")]
        public async Task<IActionResult> RevokeAll()
        {
            var users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                user.RefreshToken = null;
                await _userManager.UpdateAsync(user);
            }

            if (!await _userService.ResetAllRefreshTokens(_userManager))
            {
                return BadRequest("Smth gone wrong while reseting all refresh tokens");
            }

            return Ok();
        }


    }
}
