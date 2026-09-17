using System.ComponentModel.DataAnnotations;

namespace MasterklubAPI.DTOs.Administratori;

public record UpdateAdministratorRequest(
    [property: Required, MaxLength(50)] string Ime,
    [property: Required, MaxLength(50)] string Prezime,
    [property: Required, EmailAddress, MaxLength(100)] string Email);
