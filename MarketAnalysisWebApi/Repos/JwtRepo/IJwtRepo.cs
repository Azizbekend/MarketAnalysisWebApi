using MarketAnalysisWebApi.DbEntities.DbEntities;
using MarketAnalysisWebApi.DTOs;
using System.Security.Claims;

namespace MarketAnalysisWebApi.Repos.JwtRepo
{
    public interface IJwtRepo
    {
        Task<AuthResponseDTO> GenerateTokensAsync(DbUser user, string ipAddress, string userAgent);
        Task<AuthResponseDTO> RefreshTokenAsync(string refreshToken, string ipAddress, string userAgent);
        Task RevokeUserTokensAsync(Guid userId);
        ClaimsPrincipal? ValidateToken(string token);
    }
}
