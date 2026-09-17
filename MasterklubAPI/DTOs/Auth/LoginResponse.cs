namespace MasterklubAPI.DTOs.Auth;

public record LoginResponse(
    string Token,
    DateTime IstekTokena,
    int UserId,
    string Email,
    string Uloga);
