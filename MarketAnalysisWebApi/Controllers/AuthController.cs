using MarketAnalysisWebApi.DbEntities;
using MarketAnalysisWebApi.DbEntities.DbEntities;
using MarketAnalysisWebApi.DTOs.AuthDTOs;
using MarketAnalysisWebApi.DTOs.UserDTOs;
using MarketAnalysisWebApi.Repos.JwtRepo;
using MarketAnalysisWebApi.Repos.UserRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MarketAnalysisWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IJwtRepo _jwtRepo;
        private readonly IUserRepo _userRepo;
        private readonly IPasswordHasher<DbUser> _passwordHasher = new PasswordHasher<DbUser>();

        //public AuthController(AppDbContext context, IJwtRepo jwtService, IPasswordHasher<DbUser> passwordHasher)
        //{
        //    _context = context;
        //    _jwtRepo = jwtService;
        //    _passwordHasher = passwordHasher;
        //}
        public AuthController(AppDbContext context, IJwtRepo jwtService, IUserRepo userRepo)
        {
            _context = context;
            _jwtRepo = jwtService;
            _userRepo = userRepo;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDTO>> Register(RegisterRequestDTO request)
        {
            // Проверяем, существует ли пользователь
            try
            {
                if (await _context.UsersTable.AnyAsync(u => u.Email == request.Email))
            {
                return BadRequest("User with this email already exists");
            }

            var role = await _context.UsersRolesTable.FirstOrDefaultAsync(x => x.RoleName == request.RoleName);
            var user = new DbUser
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                FullName = request.FullName,
                PhoneNumber = request.PhoneNumber,
                RoleId = Guid.Parse(role.Id.ToString()), // Получите реальный ID роли
            };

            user.Password = _passwordHasher.HashPassword(user, request.Password);

            await _userRepo.Create(user);

            // Получаем роль пользователя для токена
            await _context.Entry(user).Reference(u => u.UserRole).LoadAsync();

            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            var userAgent = Request.Headers["User-Agent"].ToString();

            var response = await _jwtRepo.GenerateTokensAsync(user, ipAddress, userAgent);

            SetRefreshTokenCookie(response.RefreshToken, response.ExpiresAt);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost("employe/register")]
        public async Task<IActionResult> EmployeRegistration(EmployeCreateDTO dto)
        {
            try
            {
                var res = await _userRepo.CreateEmployeUser(dto);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDTO>> Login(LoginRequestDTO request)
        {
            var user = await _context.UsersTable
                .Include(u => u.UserRole)
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
            {
                return Unauthorized("Invalid credentials");
            }

            var verificationResult = _passwordHasher.VerifyHashedPassword(
                user, user.Password, request.Password);

            if (verificationResult == PasswordVerificationResult.Failed)
            {
                return Unauthorized("Invalid credentials");
            }

            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            var userAgent = Request.Headers["User-Agent"].ToString();

            var response = await _jwtRepo.GenerateTokensAsync(user, ipAddress, userAgent);

            SetRefreshTokenCookie(response.RefreshToken, response.ExpiresAt);

            return Ok(response);
        }

        [HttpGet("employer/account/")]
        public async Task<IActionResult> GetEmployerAccount(Guid userId)
        {
            try
            {
                var res =  await _userRepo.GetEmployeAccountInfoAsync(userId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("password/recovery")]
        public async Task<IActionResult> UserPasswordRecovery(string email)
        {
            try
            {
                var res = await _userRepo.PasswordRecoveryAsync(email);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("refresh")]
        public async Task<ActionResult<AuthResponseDTO>> Refresh(RefreshTokenRequestDTO request)
        {
            try
            {
                var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
                var userAgent = Request.Headers["User-Agent"].ToString();

                var response = await _jwtRepo.RefreshTokenAsync(
                    request.RefreshToken, ipAddress, userAgent);

                SetRefreshTokenCookie(response.RefreshToken, response.ExpiresAt);

                return Ok(response);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Invalid refresh token");
            }
        }
        [HttpPost("inner/createRole")]
        public async void CreateRole(string roleName)
        {
            var role = new DbUserRole { RoleName = roleName }; 
            _context.UsersRolesTable.Add(role);
            await _context.SaveChangesAsync();
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            if (Guid.TryParse(userId, out var userGuid))
            {
                await _jwtRepo.RevokeUserTokensAsync(userGuid);
            }

            Response.Cookies.Delete("refreshToken");

            return Ok();
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<UserInfoDTO>> GetCurrentUser(string token)
        {
            var claims =  _jwtRepo.ValidateToken(token);
            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if (!Guid.TryParse(userId, out var userGuid))
            {
                return Unauthorized();
            }

            var user = await _context.UsersTable
                .Include(u => u.UserRole)
                .FirstOrDefaultAsync(u => u.Id == userGuid);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(new UserInfoDTO
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Role = user.UserRole?.RoleName ?? "User"
            });
        }

        private void SetRefreshTokenCookie(string refreshToken, DateTime expiresAt)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = expiresAt.AddDays(7) // Refresh token expiration
            };

            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
    }
}
