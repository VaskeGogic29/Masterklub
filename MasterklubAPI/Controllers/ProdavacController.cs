using MasterklubAPI.Common;
using MasterklubAPI.DTOs.Prodavci;
using MasterklubAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MasterklubAPI.Controllers;

[ApiController]
[Route("api/prodavci")]
public class ProdavacController : ControllerBase
{
    private readonly IProdavacService _prodavacService;

    public ProdavacController(IProdavacService prodavacService)
    {
        _prodavacService = prodavacService;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<ProdavacResponse>>> GetAll([FromQuery] PaginationParameters parametri)
    {
        return Ok(await _prodavacService.GetAllAsync(parametri));
    }

    [HttpGet("top5")]
    public async Task<ActionResult<IEnumerable<ProdavacResponse>>> GetTop5()
    {
        return Ok(await _prodavacService.GetTop5Async());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProdavacResponse>> GetById(int id)
    {
        return Ok(await _prodavacService.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<ActionResult<ProdavacResponse>> Create(CreateProdavacRequest request)
    {
        var noviProdavac = await _prodavacService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = noviProdavac.Id }, noviProdavac);
    }

    // PRIVREMENO: prodavacId zamenjuje buduće JWT claim-ove - kasnije će ovaj endpoint postati "moj profil".
    [HttpPut("{prodavacId:int}")]
    public async Task<ActionResult<ProdavacResponse>> UpdateSelf(int prodavacId, UpdateProdavacRequest request)
    {
        return Ok(await _prodavacService.UpdateSelfAsync(prodavacId, request));
    }

    [HttpPut("{prodavacId:int}/administracija")]
    public async Task<ActionResult<ProdavacResponse>> AdminUpdate(int prodavacId, AdminUpdateProdavacRequest request)
    {
        return Ok(await _prodavacService.AdminUpdateAsync(prodavacId, request));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _prodavacService.DeleteAsync(id);
        return NoContent();
    }
}
