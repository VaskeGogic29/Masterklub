using Masterklub.Domain.Enums;

namespace MasterklubAPI.DTOs.Prodavci;

public record CreateProdavacRequest(string Ime, string Prezime, string Email, string Lozinka, TipProdavca TipProdavca);
