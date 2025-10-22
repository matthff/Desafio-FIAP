using System.Net;
using Api.Domain.DTO.Turma;
using Api.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

/// <summary>
/// Gerenciamento de alunos - V1
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Tags("Alunos")]
public class TurmaController : ControllerBase
{
    private ITurmaService _turmaService;

    public TurmaController(ITurmaService turmaService)
    {
        _turmaService = turmaService;
    }

    /// <summary>
    /// Lista todas as turmas com a quantidade de alunos em cada uma.
    /// </summary>
    /// <remarks>
    /// Retorna uma lista paginada de todos os turmas cadastradas no sistema.
    /// 
    /// Exemplo de requisição:
    /// 
    ///     GET /api/v1/turma
    /// 
    /// </remarks>
    /// <response code="200">Lista de alunos retornada com sucesso</response>
    /// <response code="400">Requisição inválida</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet(Name = "ObterTurmas")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

    /// <summary>
    /// Cadastra uma nova turma
    /// </summary>
    /// <param name="turma">Dados da turma a ser cadastrada</param>
    /// <response code="201">Turma criada com sucesso</response>
    /// <response code="400">Requisição inválida</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> InserirTurma([FromBody] TurmaCreateDto turma)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var result = await _turmaService.InserirTurma(turma);
            if (result != null)
            {
                return Created(); //TODO: Adicionar a rota de get para retornar a turma criada
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        catch (ArgumentException e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
        }
    }
}
