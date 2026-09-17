using System.ComponentModel.DataAnnotations;

namespace MasterklubAPI.DTOs.Prodavci;

public record UpdateProdavacRequest(
    [Required, MaxLength(50)] string Ime,
    [Required, MaxLength(50)] string Prezime,
    [Required, EmailAddress, MaxLength(100)] string Email);
