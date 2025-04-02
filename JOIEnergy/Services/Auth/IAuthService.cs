using System.Threading.Tasks;

namespace JOIEnergy.Services.Auth
{
    public interface IAuthService
    {
        Task<ApiResponse> ValidateCredentialsAsync(string username, string password);
        string GenerateJwtToken(string username);
        Task<ApiResponse> ValidateTokenAsync(string token);
    }
} 