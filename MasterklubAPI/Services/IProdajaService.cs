using MasterklubAPI.DTOs.Prodaje;

namespace MasterklubAPI.Services;

public interface IProdajaService
{
    Task<IEnumerable<ProdajaResponse>> GetAllAsync();
    Task<ProdajaResponse> GetByIdAsync(int id);
    Task<IEnumerable<ProdajaResponse>> GetZaProdavcaAsync(int prodavacId);
    Task<ProdajaResponse> PrijaviProdajuAsync(int prodavacId, CreateProdajaRequest request);
}
