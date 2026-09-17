using System.ComponentModel.DataAnnotations;

namespace MasterklubAPI.DTOs.Prodaje;

public record CreateProdajaRequest(
    [Range(1, int.MaxValue)] int ProizvodId,
    [Range(1, int.MaxValue)] int Kolicina);
