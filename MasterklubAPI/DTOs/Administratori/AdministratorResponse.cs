using Masterklub.Domain.Enums;

namespace MasterklubAPI.DTOs.Administratori;

public record AdministratorResponse(
    int Id,
    string Ime,
    string Prezime,
    string Email,
    StatusEntiteta Status);
