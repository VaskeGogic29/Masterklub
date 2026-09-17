using System.ComponentModel.DataAnnotations;

namespace MasterklubAPI.DTOs.Prodavci;

public record UpdateProdavacRequest(
    [property: Required, MaxLength(50)] string Ime,
    [property: Required, MaxLength(50)] string Prezime,
    [property: Required, EmailAddress, MaxLength(100)] string Email);
