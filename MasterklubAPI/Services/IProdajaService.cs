using MasterklubAPI.Common;
using MasterklubAPI.DTOs.Prodaje;
using MasterklubAPI.DTOs.Proizvodi;

namespace MasterklubAPI.Services;

public interface IProdajaService
{
    Task<PagedResult<ProdajaResponse>> GetAllAsync(PaginationParameters parametri);
    Task<ProdajaResponse> GetByIdAsync(int id);
    Task<PagedResult<ProdajaResponse>> GetZaProdavcaAsync(int prodavacId, PaginationParameters parametri);
    Task<ProdajaResponse> PrijaviProdajuAsync(int prodavacId, CreateProdajaRequest request);
    Task<IEnumerable<NajprodavanijiProizvodResponse>> GetTop5NajprodavanijihProizvodaAsync();
}
