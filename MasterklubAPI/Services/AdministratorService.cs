using Masterklub.Domain.Entities;
using Masterklub.Domain.Interfaces;
using MasterklubAPI.Common;
using MasterklubAPI.DTOs.Administratori;
using MasterklubAPI.Exceptions;

namespace MasterklubAPI.Services;

public class AdministratorService : IAdministratorService
{
    private readonly IUnitOfWork _unitOfWork;

    public AdministratorService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PagedResult<AdministratorResponse>> GetAllAsync(PaginationParameters parametri)
    {
        var administratori = await _unitOfWork.Administratori.GetAllAsync();
        return administratori.Select(MapToResponse).ToPagedResult(parametri);
    }

    public async Task<AdministratorResponse> GetByIdAsync(int id)
    {
        var administrator = await _unitOfWork.Administratori.GetByIdAsync(id)
            ?? throw new NotFoundException($"Administrator sa Id {id} ne postoji.");

        return MapToResponse(administrator);
    }

    public async Task<AdministratorResponse> CreateAsync(CreateAdministratorRequest request)
    {
        var administrator = new Administrator
        {
            Ime = request.Ime,
            Prezime = request.Prezime,
            Email = request.Email
        };

        await _unitOfWork.Administratori.AddAsync(administrator);
        await _unitOfWork.SaveChangesAsync();

        return MapToResponse(administrator);
    }

    public async Task<AdministratorResponse> UpdateAsync(int ciljniId, int trenutniKorisnikId, UpdateAdministratorRequest request)
    {
        if (ciljniId != trenutniKorisnikId)
            throw new InvalidOperationException("Administrator može da menja samo svoje podatke.");

        var administrator = await _unitOfWork.Administratori.GetByIdAsync(ciljniId)
            ?? throw new NotFoundException($"Administrator sa Id {ciljniId} ne postoji.");

        administrator.Ime = request.Ime;
        administrator.Prezime = request.Prezime;
        administrator.Email = request.Email;

        _unitOfWork.Administratori.Update(administrator);
        await _unitOfWork.SaveChangesAsync();

        return MapToResponse(administrator);
    }

    public async Task DeleteAsync(int ciljniId, int trenutniKorisnikId)
    {
        if (ciljniId == trenutniKorisnikId)
            throw new InvalidOperationException("Administrator ne sme da obriše samog sebe.");

        var administrator = await _unitOfWork.Administratori.GetByIdAsync(ciljniId)
            ?? throw new NotFoundException($"Administrator sa Id {ciljniId} ne postoji.");

        _unitOfWork.Administratori.Remove(administrator);
        await _unitOfWork.SaveChangesAsync();
    }

    private static AdministratorResponse MapToResponse(Administrator administrator) =>
        new(administrator.Id, administrator.Ime, administrator.Prezime, administrator.Email, administrator.Status);
}
