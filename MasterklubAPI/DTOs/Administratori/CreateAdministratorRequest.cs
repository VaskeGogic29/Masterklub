using System.ComponentModel.DataAnnotations;

namespace MasterklubAPI.DTOs.Administratori;

public record CreateAdministratorRequest(
    [Required, MaxLength(50)] string Ime,
    [Required, MaxLength(50)] string Prezime,
    [Required, EmailAddress, MaxLength(100)] string Email,
    [Required, MinLength(6), MaxLength(100)] string Lozinka);
