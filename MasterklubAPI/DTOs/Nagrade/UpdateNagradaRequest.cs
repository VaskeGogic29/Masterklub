using System.ComponentModel.DataAnnotations;
using Masterklub.Domain.Enums;

namespace MasterklubAPI.DTOs.Nagrade;

public record UpdateNagradaRequest(
    [property: Required, MaxLength(100)] string Naziv,
    [property: Range(1, int.MaxValue)] int BrojPoena,
    [property: Range(1, 5)] int Nivo,
    [property: EnumDataType(typeof(StatusEntiteta))] StatusEntiteta Status);
