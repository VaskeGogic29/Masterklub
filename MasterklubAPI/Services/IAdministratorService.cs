using MasterklubAPI.DTOs.Administratori;

namespace MasterklubAPI.Services;

public interface IAdministratorService
{
    Task<IEnumerable<AdministratorResponse>> GetAllAsync();
    Task<AdministratorResponse> GetByIdAsync(int id);
    Task<AdministratorResponse> CreateAsync(CreateAdministratorRequest request);
    Task<AdministratorResponse> UpdateAsync(int ciljniId, int trenutniKorisnikId, UpdateAdministratorRequest request);
    Task DeleteAsync(int ciljniId, int trenutniKorisnikId);
}
