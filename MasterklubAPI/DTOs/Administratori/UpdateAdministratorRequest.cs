using System.ComponentModel.DataAnnotations;

namespace MasterklubAPI.DTOs.Administratori;

public record UpdateAdministratorRequest(
    [Required, MaxLength(50)] string Ime,
    [Required, MaxLength(50)] string Prezime,
    [Required, EmailAddress, MaxLength(100)] string Email);
