using System.Net;
using Api.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Route("[controller]")]
public class TurmaController : ControllerBase
{
    private ITurmaService _turmaService;

    public TurmaController(ITurmaService turmaService)
    {
        _turmaService = turmaService;
    }

    [HttpGet(Name = "ObterTurmas")]
    public async Task<IActionResult> ObterTurmas()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var result = await _turmaService.ObterTodosComQuantidadeDeAlunos();
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        catch (ArgumentException e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
        }
    }
}
