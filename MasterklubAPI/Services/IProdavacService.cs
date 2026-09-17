using MasterklubAPI.Common;
using MasterklubAPI.DTOs.Prodavci;

namespace MasterklubAPI.Services;

public interface IProdavacService
{
    Task<PagedResult<ProdavacResponse>> GetAllAsync(PaginationParameters parametri);
    Task<ProdavacResponse> GetByIdAsync(int id);
    Task<IEnumerable<ProdavacResponse>> GetTop5Async();
    Task<ProdavacResponse> CreateAsync(CreateProdavacRequest request);
    Task<ProdavacResponse> UpdateSelfAsync(int prodavacId, UpdateProdavacRequest request);
    Task<ProdavacResponse> AdminUpdateAsync(int prodavacId, AdminUpdateProdavacRequest request);
    Task DeleteAsync(int prodavacId);
}
