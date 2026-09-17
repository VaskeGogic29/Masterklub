using MasterklubAPI.Auth;
using MasterklubAPI.Common;
using MasterklubAPI.DTOs.NarudzbineNagrada;
using MasterklubAPI.Extensions;
using MasterklubAPI.Middleware;
using MasterklubAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MasterklubAPI.Controllers;

[ApiController]
[Route("api/narudzbine-nagrada")]
[Authorize]
public class NarudzbinaNagradeController : ControllerBase
{
    private readonly INarudzbinaNagradeService _narudzbinaNagradeService;

    public NarudzbinaNagradeController(INarudzbinaNagradeService narudzbinaNagradeService)
    {
        _narudzbinaNagradeService = narudzbinaNagradeService;
    }

    [Authorize(Roles = Uloge.Administrator)]
    [HttpGet]
    public async Task<ActionResult<PagedResult<NarudzbinaNagradeResponse>>> GetAll([FromQuery] PaginationParameters parametri)
    {
        return Ok(await _narudzbinaNagradeService.GetAllAsync(parametri));
    }

    [Authorize(Roles = Uloge.Administrator)]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<NarudzbinaNagradeResponse>> GetById(int id)
    {
        return Ok(await _narudzbinaNagradeService.GetByIdAsync(id));
    }

    [Authorize(Roles = Uloge.Prodavac)]
    [HttpGet("moje")]
    public async Task<ActionResult<PagedResult<NarudzbinaNagradeResponse>>> GetMoje([FromQuery] PaginationParameters parametri)
    {
        var prodavacId = User.GetKorisnikId();
        return Ok(await _narudzbinaNagradeService.GetZaProdavcaAsync(prodavacId, parametri));
    }

    [Authorize(Roles = Uloge.Prodavac)]
    [RequireIdempotencyKey]
    [HttpPost("moje")]
    public async Task<ActionResult<NarudzbinaNagradeResponse>> NaruciNagradu(CreateNarudzbinaNagradeRequest request)
    {
        var prodavacId = User.GetKorisnikId();
        var novaNarudzbina = await _narudzbinaNagradeService.NaruciNagraduAsync(prodavacId, request);
        return CreatedAtAction(nameof(GetById), new { id = novaNarudzbina.Id }, novaNarudzbina);
    }
}
