using MasterklubAPI.Auth;
using MasterklubAPI.Common;
using MasterklubAPI.DTOs.Administratori;
using MasterklubAPI.Extensions;
using MasterklubAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MasterklubAPI.Controllers;

[ApiController]
[Route("api/administratori")]
[Authorize(Roles = Uloge.Administrator)]
public class AdministratorController : ControllerBase
{
    private readonly IAdministratorService _administratorService;

    public AdministratorController(IAdministratorService administratorService)
    {
        _administratorService = administratorService;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<AdministratorResponse>>> GetAll([FromQuery] PaginationParameters parametri)
    {
        return Ok(await _administratorService.GetAllAsync(parametri));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AdministratorResponse>> GetById(int id)
    {
        return Ok(await _administratorService.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<ActionResult<AdministratorResponse>> Create(CreateAdministratorRequest request)
    {
        var noviAdministrator = await _administratorService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = noviAdministrator.Id }, noviAdministrator);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<AdministratorResponse>> Update(int id, UpdateAdministratorRequest request)
    {
        var trenutniKorisnikId = User.GetKorisnikId();
        return Ok(await _administratorService.UpdateAsync(id, trenutniKorisnikId, request));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var trenutniKorisnikId = User.GetKorisnikId();
        await _administratorService.DeleteAsync(id, trenutniKorisnikId);
        return NoContent();
    }
}
