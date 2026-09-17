using System.ComponentModel.DataAnnotations;

namespace MasterklubAPI.DTOs.Nagrade;

public record CreateNagradaRequest(
    [property: Required, MaxLength(100)] string Naziv,
    [property: Range(1, int.MaxValue)] int BrojPoena,
    [property: Range(1, 5)] int Nivo);
