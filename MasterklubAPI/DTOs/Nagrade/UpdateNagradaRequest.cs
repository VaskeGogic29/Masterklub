using System.ComponentModel.DataAnnotations;
using Masterklub.Domain.Enums;

namespace MasterklubAPI.DTOs.Nagrade;

public record UpdateNagradaRequest(
    [Required, MaxLength(100)] string Naziv,
    [Range(1, int.MaxValue)] int BrojPoena,
    [Range(1, 5)] int Nivo,
    [EnumDataType(typeof(StatusEntiteta))] StatusEntiteta Status);
