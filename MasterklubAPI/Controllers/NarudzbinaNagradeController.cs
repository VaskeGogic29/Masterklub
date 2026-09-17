using MasterklubAPI.Common;
using MasterklubAPI.DTOs.NarudzbineNagrada;
using MasterklubAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MasterklubAPI.Controllers;

[ApiController]
[Route("api/narudzbine-nagrada")]
public class NarudzbinaNagradeController : ControllerBase
{
    private readonly INarudzbinaNagradeService _narudzbinaNagradeService;

    public NarudzbinaNagradeController(INarudzbinaNagradeService narudzbinaNagradeService)
    {
        _narudzbinaNagradeService = narudzbinaNagradeService;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<NarudzbinaNagradeResponse>>> GetAll([FromQuery] PaginationParameters parametri)
    {
        return Ok(await _narudzbinaNagradeService.GetAllAsync(parametri));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<NarudzbinaNagradeResponse>> GetById(int id)
    {
        return Ok(await _narudzbinaNagradeService.GetByIdAsync(id));
    }

    [HttpGet("prodavac/{prodavacId:int}")]
    public async Task<ActionResult<PagedResult<NarudzbinaNagradeResponse>>> GetZaProdavca(int prodavacId, [FromQuery] PaginationParameters parametri)
    {
        return Ok(await _narudzbinaNagradeService.GetZaProdavcaAsync(prodavacId, parametri));
    }

    // PRIVREMENO: prodavacId zamenjuje buduće JWT claim-ove dok autentifikacija nije implementirana.
    [HttpPost("prodavac/{prodavacId:int}")]
    public async Task<ActionResult<NarudzbinaNagradeResponse>> NaruciNagradu(int prodavacId, CreateNarudzbinaNagradeRequest request)
    {
        var novaNarudzbina = await _narudzbinaNagradeService.NaruciNagraduAsync(prodavacId, request);
        return CreatedAtAction(nameof(GetById), new { id = novaNarudzbina.Id }, novaNarudzbina);
    }
}
