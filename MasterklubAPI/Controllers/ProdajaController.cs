using MasterklubAPI.Auth;
using MasterklubAPI.Common;
using MasterklubAPI.DTOs.Prodaje;
using MasterklubAPI.DTOs.Proizvodi;
using MasterklubAPI.Extensions;
using MasterklubAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MasterklubAPI.Controllers;

[ApiController]
[Route("api/prodaje")]
[Authorize]
public class ProdajaController : ControllerBase
{
    private readonly IProdajaService _prodajaService;

    public ProdajaController(IProdajaService prodajaService)
    {
        _prodajaService = prodajaService;
    }

    [Authorize(Roles = Uloge.Administrator)]
    [HttpGet]
    public async Task<ActionResult<PagedResult<ProdajaResponse>>> GetAll([FromQuery] PaginationParameters parametri)
    {
        return Ok(await _prodajaService.GetAllAsync(parametri));
    }

    [Authorize(Roles = Uloge.Administrator)]
    [HttpGet("top5-proizvoda")]
    public async Task<ActionResult<IEnumerable<NajprodavanijiProizvodResponse>>> GetTop5Proizvoda()
    {
        return Ok(await _prodajaService.GetTop5NajprodavanijihProizvodaAsync());
    }

    [Authorize(Roles = Uloge.Administrator)]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProdajaResponse>> GetById(int id)
    {
        return Ok(await _prodajaService.GetByIdAsync(id));
    }

    [Authorize(Roles = Uloge.Prodavac)]
    [HttpGet("moje")]
    public async Task<ActionResult<PagedResult<ProdajaResponse>>> GetMoje([FromQuery] PaginationParameters parametri)
    {
        var prodavacId = User.GetKorisnikId();
        return Ok(await _prodajaService.GetZaProdavcaAsync(prodavacId, parametri));
    }

    [Authorize(Roles = Uloge.Prodavac)]
    [HttpPost("moje")]
    public async Task<ActionResult<ProdajaResponse>> PrijaviProdaju(CreateProdajaRequest request)
    {
        var prodavacId = User.GetKorisnikId();
        var novaProdaja = await _prodajaService.PrijaviProdajuAsync(prodavacId, request);
        return CreatedAtAction(nameof(GetById), new { id = novaProdaja.Id }, novaProdaja);
    }
}
