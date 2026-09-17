using System.ComponentModel.DataAnnotations;

namespace MasterklubAPI.DTOs.Auth;

public record LoginRequest(
    [property: Required, EmailAddress] string Email,
    [property: Required] string Lozinka);
