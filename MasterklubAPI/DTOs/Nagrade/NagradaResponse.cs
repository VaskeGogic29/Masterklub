using Masterklub.Domain.Enums;

namespace MasterklubAPI.DTOs.Nagrade;

public record NagradaResponse(
    int Id,
    string Naziv,
    int BrojPoena,
    int Nivo,
    StatusEntiteta Status);
