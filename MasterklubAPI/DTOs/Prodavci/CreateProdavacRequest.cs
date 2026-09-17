using System.ComponentModel.DataAnnotations;
using Masterklub.Domain.Enums;

namespace MasterklubAPI.DTOs.Prodavci;

public record CreateProdavacRequest(
    [property: Required, MaxLength(50)] string Ime,
    [property: Required, MaxLength(50)] string Prezime,
    [property: Required, EmailAddress, MaxLength(100)] string Email,
    [property: EnumDataType(typeof(TipProdavca))] TipProdavca TipProdavca);
