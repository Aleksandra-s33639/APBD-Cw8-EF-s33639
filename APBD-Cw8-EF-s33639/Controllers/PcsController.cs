using APBD_Cw8_EF_s33639.DTOs.Requests;
using APBD_Cw8_EF_s33639.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Cw8_EF_s33639.Controllers;

[ApiController]
[Route("api/pcs")]
public class PcsController : ControllerBase
{
    private readonly IPcsService _pcsService;

    public PcsController(IPcsService pcsService)
    {
        _pcsService = pcsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var pcs = await _pcsService.GetAllAsync();
        return Ok(pcs);
    }

    [HttpGet("{id:int}/components")]
    public async Task<IActionResult> GetComponents(int id)
    {
        var pc = await _pcsService.GetComponentsAsync(id);

        if (pc is null)
        {
            return NotFound();
        }

        return Ok(pc);
    }

    [HttpPost]
    public async Task<IActionResult> Create(PcRequestDto request)
    {
        var createdPc = await _pcsService.CreateAsync(request);

        return CreatedAtAction(
            nameof(GetComponents),
            new { id = createdPc.Id },
            createdPc
        );
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, PcRequestDto request)
    {
        var updatedPc = await _pcsService.UpdateAsync(id, request);

        if (updatedPc is null)
        {
            return NotFound();
        }

        return Ok(updatedPc);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _pcsService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}