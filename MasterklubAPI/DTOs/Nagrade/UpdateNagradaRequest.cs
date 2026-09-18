using Masterklub.Domain.Enums;

namespace MasterklubAPI.DTOs.Nagrade;

public record UpdateNagradaRequest(string Naziv, int BrojPoena, int Nivo, StatusEntiteta Status);
