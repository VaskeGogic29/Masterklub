using Masterklub.Domain.Entities;
using Masterklub.Domain.Interfaces;
using MasterklubAPI.Common;
using MasterklubAPI.DTOs.Nagrade;
using MasterklubAPI.Exceptions;

namespace MasterklubAPI.Services;

public class NagradaService : INagradaService
{
    private readonly IUnitOfWork _unitOfWork;

    public NagradaService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PagedResult<NagradaResponse>> GetAllAsync(PaginationParameters parametri)
    {
        var nagrade = await _unitOfWork.Nagrade.GetAllAsync();
        return nagrade.Select(MapToResponse).ToPagedResult(parametri);
    }

    public async Task<NagradaResponse> GetByIdAsync(int id)
    {
        var nagrada = await _unitOfWork.Nagrade.GetByIdAsync(id)
            ?? throw new NotFoundException($"Nagrada sa Id {id} ne postoji.");

        return MapToResponse(nagrada);
    }

    public async Task<NagradaResponse> CreateAsync(CreateNagradaRequest request)
    {
        var nagrada = new Nagrada
        {
            Naziv = request.Naziv,
            BrojPoena = request.BrojPoena,
            Nivo = request.Nivo
        };

        await _unitOfWork.Nagrade.AddAsync(nagrada);
        await _unitOfWork.SaveChangesAsync();

        return MapToResponse(nagrada);
    }

    public async Task<NagradaResponse> UpdateAsync(int id, UpdateNagradaRequest request)
    {
        var nagrada = await _unitOfWork.Nagrade.GetByIdAsync(id)
            ?? throw new NotFoundException($"Nagrada sa Id {id} ne postoji.");

        nagrada.Naziv = request.Naziv;
        nagrada.BrojPoena = request.BrojPoena;
        nagrada.Nivo = request.Nivo;
        nagrada.Status = request.Status;

        _unitOfWork.Nagrade.Update(nagrada);
        await _unitOfWork.SaveChangesAsync();

        return MapToResponse(nagrada);
    }

    public async Task DeleteAsync(int id)
    {
        var nagrada = await _unitOfWork.Nagrade.GetByIdAsync(id)
            ?? throw new NotFoundException($"Nagrada sa Id {id} ne postoji.");

        _unitOfWork.Nagrade.Remove(nagrada);
        await _unitOfWork.SaveChangesAsync();
    }

    private static NagradaResponse MapToResponse(Nagrada nagrada) =>
        new(nagrada.Id, nagrada.Naziv, nagrada.BrojPoena, nagrada.Nivo, nagrada.Status);
}
