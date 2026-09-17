using Masterklub.Domain.Entities;
using Masterklub.Domain.Enums;
using Masterklub.Domain.Interfaces;
using MasterklubAPI.Common;
using MasterklubAPI.DTOs.NarudzbineNagrada;
using MasterklubAPI.Exceptions;

namespace MasterklubAPI.Services;

public class NarudzbinaNagradeService : INarudzbinaNagradeService
{
    private readonly IUnitOfWork _unitOfWork;

    public NarudzbinaNagradeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PagedResult<NarudzbinaNagradeResponse>> GetAllAsync(PaginationParameters parametri)
    {
        var narudzbine = await _unitOfWork.NarudzbineNagrada.GetAllAsync();
        return narudzbine.Select(MapToResponse).ToPagedResult(parametri);
    }

    public async Task<NarudzbinaNagradeResponse> GetByIdAsync(int id)
    {
        var narudzbina = await _unitOfWork.NarudzbineNagrada.GetByIdAsync(id)
            ?? throw new NotFoundException($"Narudžbina nagrade sa Id {id} ne postoji.");

        return MapToResponse(narudzbina);
    }

    public async Task<PagedResult<NarudzbinaNagradeResponse>> GetZaProdavcaAsync(int prodavacId, PaginationParameters parametri)
    {
        var narudzbine = await _unitOfWork.NarudzbineNagrada.GetZaProdavcaAsync(prodavacId);
        return narudzbine.Select(MapToResponse).ToPagedResult(parametri);
    }

    // USE CASE 2: Prodavac naručuje nagradu.
    public async Task<NarudzbinaNagradeResponse> NaruciNagraduAsync(int prodavacId, CreateNarudzbinaNagradeRequest request)
    {
        // 1. Prodavac postoji i aktivan je.
        var prodavac = await _unitOfWork.Prodavci.GetByIdAsync(prodavacId)
            ?? throw new NotFoundException($"Prodavac sa Id {prodavacId} ne postoji.");

        if (prodavac.Status != StatusEntiteta.Aktivan)
            throw new InvalidOperationException("Prodavac nije aktivan i ne može da naruči nagradu.");

        // 2. Nagrada postoji i aktivna je.
        var nagrada = await _unitOfWork.Nagrade.GetByIdAsync(request.NagradaId)
            ?? throw new NotFoundException($"Nagrada sa Id {request.NagradaId} ne postoji.");

        if (nagrada.Status != StatusEntiteta.Aktivan)
            throw new InvalidOperationException("Nagrada nije aktivna i ne može biti naručena.");

        // 4-7. NarudzbinaNagrade konstruktor sam proverava nivo i dovoljnost bodova
        // (Prodavac.MozeDaNaruciNagradu) i oduzima bodove (Prodavac.OduzmiBodove).
        // Ako uslovi nisu ispunjeni, konstruktor baca InvalidOperationException.
        var narudzbina = new NarudzbinaNagrade(prodavac, nagrada);

        await _unitOfWork.NarudzbineNagrada.AddAsync(narudzbina);

        // 8. Jedan SaveChangesAsync poziv - nova NarudzbinaNagrade i izmenjen Prodavac idu u istu transakciju.
        await _unitOfWork.SaveChangesAsync();

        return MapToResponse(narudzbina, prodavac, nagrada);
    }

    private static NarudzbinaNagradeResponse MapToResponse(NarudzbinaNagrade narudzbina) => new(
        narudzbina.Id,
        narudzbina.ProdavacId,
        $"{narudzbina.Prodavac.Ime} {narudzbina.Prodavac.Prezime}",
        narudzbina.NagradaId,
        narudzbina.Nagrada.Naziv,
        narudzbina.DatumNarudzbine,
        narudzbina.BrojPoenaOduzetih);

    private static NarudzbinaNagradeResponse MapToResponse(NarudzbinaNagrade narudzbina, Prodavac prodavac, Nagrada nagrada) => new(
        narudzbina.Id,
        narudzbina.ProdavacId,
        $"{prodavac.Ime} {prodavac.Prezime}",
        narudzbina.NagradaId,
        nagrada.Naziv,
        narudzbina.DatumNarudzbine,
        narudzbina.BrojPoenaOduzetih);
}
