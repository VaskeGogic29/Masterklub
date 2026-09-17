using Masterklub.Domain.Enums;
using MasterklubAPI.Common;
using MasterklubAPI.DTOs.Proizvodi;

namespace MasterklubAPI.Services;

public interface IProizvodService
{
    Task<PagedResult<ProizvodResponse>> GetAllAsync(PaginationParameters parametri);
    Task<ProizvodResponse> GetByIdAsync(int id);
    Task<PagedResult<ProizvodResponse>> GetPoKategorijiAsync(KategorijaProizvoda kategorija, PaginationParameters parametri);
    Task<ProizvodResponse> CreateAsync(CreateProizvodRequest request);
    Task<ProizvodResponse> UpdateAsync(int id, UpdateProizvodRequest request);
    Task DeleteAsync(int id);
}
