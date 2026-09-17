using MasterklubAPI.DTOs.Prodavci;

namespace MasterklubAPI.Services;

public interface IProdavacService
{
    Task<IEnumerable<ProdavacResponse>> GetAllAsync();
    Task<ProdavacResponse> GetByIdAsync(int id);
    Task<IEnumerable<ProdavacResponse>> GetTop5Async();
    Task<ProdavacResponse> CreateAsync(CreateProdavacRequest request);
    Task<ProdavacResponse> UpdateSelfAsync(int prodavacId, UpdateProdavacRequest request);
    Task<ProdavacResponse> AdminUpdateAsync(int prodavacId, AdminUpdateProdavacRequest request);
}
