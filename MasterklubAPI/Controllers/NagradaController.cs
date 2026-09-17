using MasterklubAPI.Common;
using MasterklubAPI.DTOs.Nagrade;
using MasterklubAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MasterklubAPI.Controllers;

[ApiController]
[Route("api/nagrade")]
public class NagradaController : ControllerBase
{
    private readonly INagradaService _nagradaService;

    public NagradaController(INagradaService nagradaService)
    {
        _nagradaService = nagradaService;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<NagradaResponse>>> GetAll([FromQuery] PaginationParameters parametri)
    {
        return Ok(await _nagradaService.GetAllAsync(parametri));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<NagradaResponse>> GetById(int id)
    {
        return Ok(await _nagradaService.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<ActionResult<NagradaResponse>> Create(CreateNagradaRequest request)
    {
        var novaNagrada = await _nagradaService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = novaNagrada.Id }, novaNagrada);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<NagradaResponse>> Update(int id, UpdateNagradaRequest request)
    {
        return Ok(await _nagradaService.UpdateAsync(id, request));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _nagradaService.DeleteAsync(id);
        return NoContent();
    }
}
