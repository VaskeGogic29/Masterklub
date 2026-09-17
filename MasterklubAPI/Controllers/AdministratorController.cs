using MasterklubAPI.Common;
using MasterklubAPI.DTOs.Administratori;
using MasterklubAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MasterklubAPI.Controllers;

[ApiController]
[Route("api/administratori")]
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

    // PRIVREMENO: trenutniKorisnikId zamenjuje buduće JWT claim-ove dok autentifikacija nije implementirana.
    [HttpPut("{id:int}")]
    public async Task<ActionResult<AdministratorResponse>> Update(int id, [FromQuery] int trenutniKorisnikId, UpdateAdministratorRequest request)
    {
        return Ok(await _administratorService.UpdateAsync(id, trenutniKorisnikId, request));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, [FromQuery] int trenutniKorisnikId)
    {
        await _administratorService.DeleteAsync(id, trenutniKorisnikId);
        return NoContent();
    }
}
