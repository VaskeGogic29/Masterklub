using System.ComponentModel.DataAnnotations;

namespace MasterklubAPI.DTOs.Auth;

public record LoginRequest(
    [Required, EmailAddress] string Email,
    [Required] string Lozinka);
