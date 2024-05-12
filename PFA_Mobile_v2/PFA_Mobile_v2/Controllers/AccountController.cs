using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PFA_Mobile_v2.Application.AuthenticationInteraction.Interfaces;
using PFA_Mobile_v2.Application.Models;
using PFA_Mobile_v2.Application.Services.Interfaces;
using PFA_Mobile_v2.Domain.Entities;
using PFA_Mobile_v2.Infrastructure.Authentication.Extensions;
using PFA_Mobile_v2.Infrastructure.Authentication.Models;

namespace PFA_Mobile_v2.Controllers;

/// <summary>
/// Контроллер аккаунтов
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IUserService _userService;
    private readonly IAuthenticationService _authenticationService;
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Контроллер аккаунтов
    /// </summary>
    public AccountController(UserManager<AppUser> userManager, IUserService userService, IConfiguration configuration,
        ITokenService tokenService, IAuthenticationService authenticationService)
    {
        _userManager = userManager;
        _userService = userService;
        _tokenService = tokenService;
        _authenticationService = authenticationService;
        _configuration = configuration;
    }

    /// <summary>
    /// Аутентификация
    /// </summary>
    /// <param name="request">Модель запроса аутентификации</param>
    /// <returns>Модель ответа на запрос аутентификации</returns>
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Authenticate([FromBody] AuthRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var managedUser = await _userService.GetByLoginAsync(request.Login, _userManager);
        if (managedUser is null)
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
        {
            return Unauthorized();
        }

        var roles = await _userService.GetUserRoles(user, _userManager);

        var accessToken = await _tokenService.CreateToken(user, _userManager, roles);
        if (!await _authenticationService.CreateRefreshToken(user, _userManager, _configuration))
        {
            return BadRequest("Smth gone wrong while creating tokens");
        }

        return Ok(new AuthResponse
        {
            Login = user.Login,
            Token = accessToken,
            RefreshToken = user.RefreshToken!
        });
    }

    /// <summary>
    /// Регистрация
    /// </summary>
    /// <param name="request">Модель запроса аутентификации</param>
    /// <returns>Модель ответа на запрос аутентификации></returns>
    [HttpPost("register")]
    public async Task<ActionResult<AuthResponse>> Register([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(request);
        }

        var userModel = new UserDto
        {
            Login = request.Login,
        };

        var result = await _userService.CreateAsync(userModel, request.Password, _userManager);
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        if (!result.Succeeded)
        {
            return BadRequest(request);
        }

        var findUser = await _userService.GetByLoginAsync(request.Login, _userManager);
        if (findUser is null)
        {
            return BadRequest(request);
        }

        await _userService.AddToRoleAsync(findUser, "Member", _userManager);

        return await Authenticate(new AuthRequest
        {
            Login = request.Login,
            Password = request.Password
        });
    }

    /// <summary>
    /// Обновление токенов
    /// </summary>
    /// <param name="tokenModel">Access и refresh токены</param>
    /// <returns></returns>
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

        var login = principal?.Identity?.Name;
        var user = await _userService.GetByLoginAsync(login!, _userManager);
        if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            return BadRequest("Invalid access token or refresh token");
        }

        var newAccessToken = _configuration.CreateToken(principal?.Claims.ToList());
        var newRefreshToken = _authenticationService.CreateRefreshToken(user, _userManager, _configuration);

        return new ObjectResult(new
        {
            accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            refreshToken = newRefreshToken
        });
    }

    /// <summary>
    /// Сброс refresh токена пользователя
    /// </summary>
    /// <param name="login">Логин пользователя</param>
    /// <returns></returns>
    [Authorize]
    [HttpPost]
    [Route("revoke/{login}")]
    public async Task<IActionResult> Revoke(string login)
    {
        var user = await _userService.GetByLoginAsync(login, _userManager);

        if (user is null)
        {
            return BadRequest("Invalid user login");
        }

        if (!await _authenticationService.ResetRefreshToken(user, _userManager))
        {
            return BadRequest("Smth gone wrong while reseting refresh token");
        }

        return Ok();
    }

    /// <summary>
    /// Сброс refresh токенов всех пользователей
    /// </summary>
    /// <returns></returns>
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

        if (!await _authenticationService.ResetAllRefreshTokens(_userManager))
        {
            return BadRequest("Smth gone wrong while reseting all refresh tokens");
        }

        return Ok();
    }

    /// <summary>
    /// Получение пользователя по его логину
    /// </summary>
    /// <param name="login">Логин пользователя</param>
    /// <returns>Пользователь</returns>
    [Authorize]
    [HttpGet]
    [Route("user/{login}")]
    public async Task<ActionResult<UserDto>> GetUserByLogin(string login)
    {
        var user = await _userService.GetByLoginAsync(login, _userManager);

        if (user is null)
        {
            return BadRequest("Invalid user login");
        }

        return user;
    }
}