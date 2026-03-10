using MarketAnalysisWebApi.DbEntities;
using MarketAnalysisWebApi.DbEntities.DbEntities;
using MarketAnalysisWebApi.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MarketAnalysisWebApi.Repos.JwtRepo
{
    public class JwtSettings
    {
        public string? SecretKey { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public int AccessTokenExpirationMinutes { get; set; }
        public int RefreshTokenExpirationDays { get; set; }
    }
    public class JwtRepo : IJwtRepo
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;
        private readonly byte[] _secretKey;
        private readonly JwtSettings _jwtSettings;

        public JwtRepo(IConfiguration configuration, AppDbContext context)
        {
            _configuration = configuration;
            _context = context;
            _jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
            _secretKey = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);
        }

        public async Task<AuthResponseDTO> GenerateTokensAsync(DbUser user, string ipAddress, string userAgent)
        {
            var accessToken = GenerateAccessToken(user);
            var refreshToken = await GenerateRefreshTokenAsync(user, ipAddress, userAgent);

            return new AuthResponseDTO
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                ExpiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes),
                User = MapToUserInfo(user)
            };
        }

        private string GenerateAccessToken(DbUser user)
        {
            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("fullName", user.FullName ?? ""),
            new Claim("role", user.UserRole?.RoleName ?? "User"),
            new Claim("phone", user.PhoneNumber ?? "")
        };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(_secretKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private async Task<DbRefreshToken> GenerateRefreshTokenAsync(DbUser user, string ipAddress, string userAgent)
        {
            var refreshToken = new DbRefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray()),
                ExpiryDate = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays),
                IpAddress = ipAddress,
                UserAgent = userAgent,
                CreatedAt = DateTime.UtcNow
            };

            // Удаляем старые refresh токены пользователя (опционально)
            var oldTokens = _context.RefreshTokens
                .Where(rt => rt.UserId == user.Id && rt.ExpiryDate < DateTime.UtcNow);
            _context.RefreshTokens.RemoveRange(oldTokens);

            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();

            return refreshToken;
        }

        public async Task<AuthResponseDTO> RefreshTokenAsync(string refreshToken, string ipAddress, string userAgent)
        {
            var token = await _context.RefreshTokens
                .Include(rt => rt.User)
                .ThenInclude(u => u.UserRole)
                .FirstOrDefaultAsync(rt => rt.Token == refreshToken);

            if (token == null || token.ExpiryDate < DateTime.UtcNow || token.IsRevoked)
            {
                throw new UnauthorizedAccessException("Invalid refresh token");
            }

            // Помечаем старый токен как использованный
            token.IsRevoked = true;
            await _context.SaveChangesAsync();

            // Генерируем новые токены
            return await GenerateTokensAsync(token.User, ipAddress, userAgent);
        }

        public async Task RevokeUserTokensAsync(Guid userId)
        {
            var tokens = await _context.RefreshTokens
                .Where(rt => rt.UserId == userId && !rt.IsRevoked).ToListAsync();

            foreach (var token in tokens)
            {
                token.IsRevoked = true;
            }

            await _context.SaveChangesAsync();
        }

        public ClaimsPrincipal? ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(_secretKey),
                    ValidateIssuer = true,
                    ValidIssuer = _jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = _jwtSettings.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out _);

                return principal;
            }
            catch
            {
                return null;
            }
        }

        private UserInfoDTO MapToUserInfo(DbUser user)
        {
            return new UserInfoDTO
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Role = user.UserRole?.RoleName ?? "User"
            };
        }
    }
}
