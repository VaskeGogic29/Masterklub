using MasterklubAPI.DTOs.NarudzbineNagrada;

namespace MasterklubAPI.Services;

public interface INarudzbinaNagradeService
{
    Task<IEnumerable<NarudzbinaNagradeResponse>> GetAllAsync();
    Task<NarudzbinaNagradeResponse> GetByIdAsync(int id);
    Task<IEnumerable<NarudzbinaNagradeResponse>> GetZaProdavcaAsync(int prodavacId);
    Task<NarudzbinaNagradeResponse> NaruciNagraduAsync(int prodavacId, CreateNarudzbinaNagradeRequest request);
}
