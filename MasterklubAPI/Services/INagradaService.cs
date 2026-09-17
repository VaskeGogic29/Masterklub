using MasterklubAPI.Common;
using MasterklubAPI.DTOs.Nagrade;

namespace MasterklubAPI.Services;

public interface INagradaService
{
    Task<PagedResult<NagradaResponse>> GetAllAsync(PaginationParameters parametri);
    Task<NagradaResponse> GetByIdAsync(int id);
    Task<NagradaResponse> CreateAsync(CreateNagradaRequest request);
    Task<NagradaResponse> UpdateAsync(int id, UpdateNagradaRequest request);
    Task DeleteAsync(int id);
}
