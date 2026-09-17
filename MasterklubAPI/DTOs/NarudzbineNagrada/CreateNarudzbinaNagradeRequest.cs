using System.ComponentModel.DataAnnotations;

namespace MasterklubAPI.DTOs.NarudzbineNagrada;

public record CreateNarudzbinaNagradeRequest(
    [Range(1, int.MaxValue)] int NagradaId);
