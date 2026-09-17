using Masterklub.Domain.Enums;
using MasterklubAPI.DTOs.Proizvodi;

namespace MasterklubAPI.Services;

public interface IProizvodService
{
    Task<IEnumerable<ProizvodResponse>> GetAllAsync();
    Task<ProizvodResponse> GetByIdAsync(int id);
    Task<IEnumerable<ProizvodResponse>> GetPoKategorijiAsync(KategorijaProizvoda kategorija);
    Task<ProizvodResponse> CreateAsync(CreateProizvodRequest request);
    Task<ProizvodResponse> UpdateAsync(int id, UpdateProizvodRequest request);
    Task DeleteAsync(int id);
}
