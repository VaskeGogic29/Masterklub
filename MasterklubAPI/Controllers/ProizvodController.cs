using Masterklub.Domain.Enums;
using MasterklubAPI.Common;
using MasterklubAPI.DTOs.Proizvodi;
using MasterklubAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MasterklubAPI.Controllers;

[ApiController]
[Route("api/proizvodi")]
public class ProizvodController : ControllerBase
{
    private readonly IProizvodService _proizvodService;

    public ProizvodController(IProizvodService proizvodService)
    {
        _proizvodService = proizvodService;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<ProizvodResponse>>> GetAll([FromQuery] PaginationParameters parametri)
    {
        return Ok(await _proizvodService.GetAllAsync(parametri));
    }

    [HttpGet("kategorija/{kategorija}")]
    public async Task<ActionResult<PagedResult<ProizvodResponse>>> GetPoKategoriji(KategorijaProizvoda kategorija, [FromQuery] PaginationParameters parametri)
    {
        return Ok(await _proizvodService.GetPoKategorijiAsync(kategorija, parametri));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProizvodResponse>> GetById(int id)
    {
        return Ok(await _proizvodService.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<ActionResult<ProizvodResponse>> Create(CreateProizvodRequest request)
    {
        var noviProizvod = await _proizvodService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = noviProizvod.Id }, noviProizvod);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ProizvodResponse>> Update(int id, UpdateProizvodRequest request)
    {
        return Ok(await _proizvodService.UpdateAsync(id, request));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _proizvodService.DeleteAsync(id);
        return NoContent();
    }
}
