using Masterklub.Domain.Entities;
using Masterklub.Domain.Enums;
using Masterklub.Domain.Interfaces;
using MasterklubAPI.DTOs.Proizvodi;
using MasterklubAPI.Exceptions;

namespace MasterklubAPI.Services;

public class ProizvodService : IProizvodService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProizvodService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ProizvodResponse>> GetAllAsync()
    {
        var proizvodi = await _unitOfWork.Proizvodi.GetAllAsync();
        return proizvodi.Select(MapToResponse);
    }

    public async Task<ProizvodResponse> GetByIdAsync(int id)
    {
        var proizvod = await _unitOfWork.Proizvodi.GetByIdAsync(id)
            ?? throw new NotFoundException($"Proizvod sa Id {id} ne postoji.");

        return MapToResponse(proizvod);
    }

    public async Task<IEnumerable<ProizvodResponse>> GetPoKategorijiAsync(KategorijaProizvoda kategorija)
    {
        var proizvodi = await _unitOfWork.Proizvodi.GetPoKategorijiAsync(kategorija);
        return proizvodi.Select(MapToResponse);
    }

    public async Task<ProizvodResponse> CreateAsync(CreateProizvodRequest request)
    {
        var proizvod = new Proizvod
        {
            Naziv = request.Naziv,
            BrojBodova = request.BrojBodova,
            Kategorija = request.Kategorija
        };

        await _unitOfWork.Proizvodi.AddAsync(proizvod);
        await _unitOfWork.SaveChangesAsync();

        return MapToResponse(proizvod);
    }

    public async Task<ProizvodResponse> UpdateAsync(int id, UpdateProizvodRequest request)
    {
        var proizvod = await _unitOfWork.Proizvodi.GetByIdAsync(id)
            ?? throw new NotFoundException($"Proizvod sa Id {id} ne postoji.");

        proizvod.Naziv = request.Naziv;
        proizvod.BrojBodova = request.BrojBodova;
        proizvod.Kategorija = request.Kategorija;
        proizvod.Status = request.Status;

        _unitOfWork.Proizvodi.Update(proizvod);
        await _unitOfWork.SaveChangesAsync();

        return MapToResponse(proizvod);
    }

    public async Task DeleteAsync(int id)
    {
        var proizvod = await _unitOfWork.Proizvodi.GetByIdAsync(id)
            ?? throw new NotFoundException($"Proizvod sa Id {id} ne postoji.");

        _unitOfWork.Proizvodi.Remove(proizvod);
        await _unitOfWork.SaveChangesAsync();
    }

    private static ProizvodResponse MapToResponse(Proizvod proizvod) =>
        new(proizvod.Id, proizvod.Naziv, proizvod.BrojBodova, proizvod.Kategorija, proizvod.Status);
}
