using MasterklubAPI.Common;
using MasterklubAPI.DTOs.NarudzbineNagrada;

namespace MasterklubAPI.Services;

public interface INarudzbinaNagradeService
{
    Task<PagedResult<NarudzbinaNagradeResponse>> GetAllAsync(PaginationParameters parametri);
    Task<NarudzbinaNagradeResponse> GetByIdAsync(int id);
    Task<PagedResult<NarudzbinaNagradeResponse>> GetZaProdavcaAsync(int prodavacId, PaginationParameters parametri);
    Task<NarudzbinaNagradeResponse> NaruciNagraduAsync(int prodavacId, CreateNarudzbinaNagradeRequest request);
}
