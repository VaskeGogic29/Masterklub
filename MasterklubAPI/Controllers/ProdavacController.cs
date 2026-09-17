using MasterklubAPI.Auth;
using MasterklubAPI.Common;
using MasterklubAPI.DTOs.Prodavci;
using MasterklubAPI.Extensions;
using MasterklubAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MasterklubAPI.Controllers;

[ApiController]
[Route("api/prodavci")]
[Authorize]
public class ProdavacController : ControllerBase
{
    private readonly IProdavacService _prodavacService;

    public ProdavacController(IProdavacService prodavacService)
    {
        _prodavacService = prodavacService;
    }

    [Authorize(Roles = Uloge.Administrator)]
    [HttpGet]
    public async Task<ActionResult<PagedResult<ProdavacResponse>>> GetAll([FromQuery] PaginationParameters parametri)
    {
        return Ok(await _prodavacService.GetAllAsync(parametri));
    }

    [Authorize(Roles = Uloge.Administrator)]
    [HttpGet("top5")]
    public async Task<ActionResult<IEnumerable<ProdavacResponse>>> GetTop5()
    {
        return Ok(await _prodavacService.GetTop5Async());
    }

    [Authorize(Roles = Uloge.Administrator)]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProdavacResponse>> GetById(int id)
    {
        return Ok(await _prodavacService.GetByIdAsync(id));
    }

    [Authorize(Roles = Uloge.Administrator)]
    [HttpPost]
    public async Task<ActionResult<ProdavacResponse>> Create(CreateProdavacRequest request)
    {
        var noviProdavac = await _prodavacService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = noviProdavac.Id }, noviProdavac);
    }

    [Authorize(Roles = Uloge.Administrator)]
    [HttpPut("{prodavacId:int}/administracija")]
    public async Task<ActionResult<ProdavacResponse>> AdminUpdate(int prodavacId, AdminUpdateProdavacRequest request)
    {
        return Ok(await _prodavacService.AdminUpdateAsync(prodavacId, request));
    }

    [Authorize(Roles = Uloge.Administrator)]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _prodavacService.DeleteAsync(id);
        return NoContent();
    }

    [Authorize(Roles = Uloge.Prodavac)]
    [HttpGet("moj-profil")]
    public async Task<ActionResult<ProdavacResponse>> GetMojProfil()
    {
        var prodavacId = User.GetKorisnikId();
        return Ok(await _prodavacService.GetByIdAsync(prodavacId));
    }

    [Authorize(Roles = Uloge.Prodavac)]
    [HttpPut("moj-profil")]
    public async Task<ActionResult<ProdavacResponse>> UpdateMojProfil(UpdateProdavacRequest request)
    {
        var prodavacId = User.GetKorisnikId();
        return Ok(await _prodavacService.UpdateSelfAsync(prodavacId, request));
    }
}
