using MasterklubAPI.DTOs.Auth;

namespace MasterklubAPI.Services;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginRequest request);
}
