using Masterklub.Domain.Entities;
using Masterklub.Domain.Interfaces;
using MasterklubAPI.DTOs.Prodavci;
using MasterklubAPI.Exceptions;

namespace MasterklubAPI.Services;

public class ProdavacService : IProdavacService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProdavacService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ProdavacResponse>> GetAllAsync()
    {
        var prodavci = await _unitOfWork.Prodavci.GetAllAsync();
        return prodavci.Select(MapToResponse);
    }

    public async Task<ProdavacResponse> GetByIdAsync(int id)
    {
        var prodavac = await _unitOfWork.Prodavci.GetByIdAsync(id)
            ?? throw new NotFoundException($"Prodavac sa Id {id} ne postoji.");

        return MapToResponse(prodavac);
    }

    public async Task<IEnumerable<ProdavacResponse>> GetTop5Async()
    {
        var top5 = await _unitOfWork.Prodavci.GetTop5Async();
        return top5.Select(MapToResponse);
    }

    public async Task<ProdavacResponse> CreateAsync(CreateProdavacRequest request)
    {
        var prodavac = new Prodavac
        {
            Ime = request.Ime,
            Prezime = request.Prezime,
            Email = request.Email,
            TipProdavca = request.TipProdavca
        };

        await _unitOfWork.Prodavci.AddAsync(prodavac);
        await _unitOfWork.SaveChangesAsync();

        return MapToResponse(prodavac);
    }

    public async Task<ProdavacResponse> UpdateSelfAsync(int prodavacId, UpdateProdavacRequest request)
    {
        var prodavac = await _unitOfWork.Prodavci.GetByIdAsync(prodavacId)
            ?? throw new NotFoundException($"Prodavac sa Id {prodavacId} ne postoji.");

        prodavac.Ime = request.Ime;
        prodavac.Prezime = request.Prezime;
        prodavac.Email = request.Email;

        _unitOfWork.Prodavci.Update(prodavac);
        await _unitOfWork.SaveChangesAsync();

        return MapToResponse(prodavac);
    }

    public async Task<ProdavacResponse> AdminUpdateAsync(int prodavacId, AdminUpdateProdavacRequest request)
    {
        var prodavac = await _unitOfWork.Prodavci.GetByIdAsync(prodavacId)
            ?? throw new NotFoundException($"Prodavac sa Id {prodavacId} ne postoji.");

        prodavac.TipProdavca = request.TipProdavca;
        prodavac.Status = request.Status;

        _unitOfWork.Prodavci.Update(prodavac);
        await _unitOfWork.SaveChangesAsync();

        return MapToResponse(prodavac);
    }

    private static ProdavacResponse MapToResponse(Prodavac prodavac) => new(
        prodavac.Id,
        prodavac.Ime,
        prodavac.Prezime,
        prodavac.Email,
        prodavac.TipProdavca,
        prodavac.BrojPoena,
        prodavac.UkupnoOstvarenihBodova,
        prodavac.Nivo,
        prodavac.Status);
}
