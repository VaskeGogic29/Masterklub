using MasterklubAPI.Common;
using MasterklubAPI.DTOs.Prodaje;
using MasterklubAPI.DTOs.Proizvodi;
using MasterklubAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MasterklubAPI.Controllers;

[ApiController]
[Route("api/prodaje")]
public class ProdajaController : ControllerBase
{
    private readonly IProdajaService _prodajaService;

    public ProdajaController(IProdajaService prodajaService)
    {
        _prodajaService = prodajaService;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<ProdajaResponse>>> GetAll([FromQuery] PaginationParameters parametri)
    {
        return Ok(await _prodajaService.GetAllAsync(parametri));
    }

    [HttpGet("top5-proizvoda")]
    public async Task<ActionResult<IEnumerable<NajprodavanijiProizvodResponse>>> GetTop5Proizvoda()
    {
        return Ok(await _prodajaService.GetTop5NajprodavanijihProizvodaAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProdajaResponse>> GetById(int id)
    {
        return Ok(await _prodajaService.GetByIdAsync(id));
    }

    [HttpGet("prodavac/{prodavacId:int}")]
    public async Task<ActionResult<PagedResult<ProdajaResponse>>> GetZaProdavca(int prodavacId, [FromQuery] PaginationParameters parametri)
    {
        return Ok(await _prodajaService.GetZaProdavcaAsync(prodavacId, parametri));
    }

    // PRIVREMENO: prodavacId zamenjuje buduće JWT claim-ove dok autentifikacija nije implementirana.
    [HttpPost("prodavac/{prodavacId:int}")]
    public async Task<ActionResult<ProdajaResponse>> PrijaviProdaju(int prodavacId, CreateProdajaRequest request)
    {
        var novaProdaja = await _prodajaService.PrijaviProdajuAsync(prodavacId, request);
        return CreatedAtAction(nameof(GetById), new { id = novaProdaja.Id }, novaProdaja);
    }
}
