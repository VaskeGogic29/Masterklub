using MasterklubAPI.DTOs.Nagrade;

namespace MasterklubAPI.Services;

public interface INagradaService
{
    Task<IEnumerable<NagradaResponse>> GetAllAsync();
    Task<NagradaResponse> GetByIdAsync(int id);
    Task<NagradaResponse> CreateAsync(CreateNagradaRequest request);
    Task<NagradaResponse> UpdateAsync(int id, UpdateNagradaRequest request);
    Task DeleteAsync(int id);
}
