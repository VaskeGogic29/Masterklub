using System.ComponentModel.DataAnnotations;

namespace MasterklubAPI.DTOs.Nagrade;

public record CreateNagradaRequest(
    [Required, MaxLength(100)] string Naziv,
    [Range(1, int.MaxValue)] int BrojPoena,
    [Range(1, 5)] int Nivo);
