using System.ComponentModel.DataAnnotations;
using Masterklub.Domain.Enums;

namespace MasterklubAPI.DTOs.Prodavci;

public record CreateProdavacRequest(
    [Required, MaxLength(50)] string Ime,
    [Required, MaxLength(50)] string Prezime,
    [Required, EmailAddress, MaxLength(100)] string Email,
    [Required, MinLength(6), MaxLength(100)] string Lozinka,
    [EnumDataType(typeof(TipProdavca))] TipProdavca TipProdavca);
