using System.ComponentModel.DataAnnotations;

namespace MasterklubAPI.DTOs.Prodaje;

public record CreateProdajaRequest(
    [property: Range(1, int.MaxValue)] int ProizvodId,
    [property: Range(1, int.MaxValue)] int Kolicina);
