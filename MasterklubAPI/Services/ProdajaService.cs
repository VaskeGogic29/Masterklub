using Masterklub.Domain.Entities;
using Masterklub.Domain.Enums;
using Masterklub.Domain.Interfaces;
using MasterklubAPI.Common;
using MasterklubAPI.DTOs.Prodaje;
using MasterklubAPI.DTOs.Proizvodi;
using MasterklubAPI.Exceptions;

namespace MasterklubAPI.Services;

public class ProdajaService : IProdajaService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProdajaService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PagedResult<ProdajaResponse>> GetAllAsync(PaginationParameters parametri)
    {
        var prodaje = await _unitOfWork.Prodaje.GetAllAsync();
        return prodaje.Select(MapToResponse).ToPagedResult(parametri);
    }

    public async Task<ProdajaResponse> GetByIdAsync(int id)
    {
        var prodaja = await _unitOfWork.Prodaje.GetByIdAsync(id)
            ?? throw new NotFoundException($"Prodaja sa Id {id} ne postoji.");

        return MapToResponse(prodaja);
    }

    public async Task<PagedResult<ProdajaResponse>> GetZaProdavcaAsync(int prodavacId, PaginationParameters parametri)
    {
        var prodaje = await _unitOfWork.Prodaje.GetZaProdavcaAsync(prodavacId);
        return prodaje.Select(MapToResponse).ToPagedResult(parametri);
    }

    // USE CASE 1: Prodavac prijavljuje prodaju proizvoda.
    public async Task<ProdajaResponse> PrijaviProdajuAsync(int prodavacId, CreateProdajaRequest request)
    {
        // 1. Prodavac postoji i aktivan je.
        var prodavac = await _unitOfWork.Prodavci.GetByIdAsync(prodavacId)
            ?? throw new NotFoundException($"Prodavac sa Id {prodavacId} ne postoji.");

        if (prodavac.Status != StatusEntiteta.Aktivan)
            throw new InvalidOperationException("Prodavac nije aktivan i ne može da prijavljuje prodaju.");

        // 2. Proizvod postoji i aktivan je.
        var proizvod = await _unitOfWork.Proizvodi.GetByIdAsync(request.ProizvodId)
            ?? throw new NotFoundException($"Proizvod sa Id {request.ProizvodId} ne postoji.");

        if (proizvod.Status != StatusEntiteta.Aktivan)
            throw new InvalidOperationException("Proizvod nije aktivan i ne može biti predmet prodaje.");

        // 3-7. Prodaja konstruktor sam računa BrojBodovaOstvarenih iz proizvod.BrojBodova * Kolicina,
        // dodeljuje bodove prodavcu (Prodavac.DodajBodove) i preračunava nivo (max 5, na svakih 50 bodova).
        var prodaja = new Prodaja(prodavac, proizvod, request.Kolicina);

        await _unitOfWork.Prodaje.AddAsync(prodaja);

        // 9. Jedan SaveChangesAsync poziv - nova Prodaja i izmenjen Prodavac idu u istu transakciju.
        await _unitOfWork.SaveChangesAsync();

        return MapToResponse(prodaja, prodavac, proizvod);
    }

    public async Task<IEnumerable<NajprodavanijiProizvodResponse>> GetTop5NajprodavanijihProizvodaAsync()
    {
        var top5 = await _unitOfWork.Prodaje.GetTop5NajprodavanijihProizvodaAsync();
        return top5.Select(stavka => new NajprodavanijiProizvodResponse(
            stavka.Proizvod.Id,
            stavka.Proizvod.Naziv,
            stavka.UkupnoProdatihKomada));
    }

    private static ProdajaResponse MapToResponse(Prodaja prodaja) => new(
        prodaja.Id,
        prodaja.ProdavacId,
        $"{prodaja.Prodavac.Ime} {prodaja.Prodavac.Prezime}",
        prodaja.ProizvodId,
        prodaja.Proizvod.Naziv,
        prodaja.Kolicina,
        prodaja.DatumProdaje,
        prodaja.BrojBodovaOstvarenih);

    private static ProdajaResponse MapToResponse(Prodaja prodaja, Prodavac prodavac, Proizvod proizvod) => new(
        prodaja.Id,
        prodaja.ProdavacId,
        $"{prodavac.Ime} {prodavac.Prezime}",
        prodaja.ProizvodId,
        proizvod.Naziv,
        prodaja.Kolicina,
        prodaja.DatumProdaje,
        prodaja.BrojBodovaOstvarenih);
}
