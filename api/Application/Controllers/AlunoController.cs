using System.Net;
using Api.Domain.DTO.Aluno;
using Api.Domain.DTO.Turma;
using Api.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

/// <summary>
/// Gerenciamento de alunos - V1
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Tags("Aluno")]
public class AlunoController : ControllerBase
{
    private readonly IAlunoService _alunoService;

    public AlunoController(IAlunoService alunoService)
    {
        _alunoService = alunoService;
    }

    /// <summary>
    /// Lista todos os alunos.
    /// </summary>
    /// <remarks>
    /// Retorna uma lista paginada ordenada por ordem alfabética pelo nome de todos os alunos cadastrados no sistema.
    /// </remarks>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ObterTodosAlunosOrdenadosPorNome([FromQuery] int page)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var result = await _alunoService.ObterTodosOrdenadosPorNome(page, Pagination.DefaultPageSize);
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
    /// Obter um aluno pelo seu identificador.
    /// </summary>
    /// <remarks>
    /// Retorna um objeto com as informações sobre o aluno.
    /// </remarks>
    /// <param name="alunoId">Identificador do aluno.</param>
    [HttpGet("{alunoId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ObterAlunoPorId(int alunoId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var result = await _alunoService.ObterPorIdAsync(alunoId);
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
    /// Obter um aluno pelo seu nome.
    /// </summary>
    /// <remarks>
    /// Retorna um objeto com as informações sobre o aluno.
    /// </remarks>
    /// <param name="alunoNome">Nome do aluno.</param>
    [HttpGet()]
    [Route("nome/{alunoNome}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ObterAlunoPorNome(string alunoNome)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var result = await _alunoService.ObterPorIdComNome(alunoNome);
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
    /// Obter um aluno pelo seu CPF.
    /// </summary>
    /// <remarks>
    /// Retorna um objeto com as informações sobre o aluno.
    /// </remarks>
    /// <param name="alunoCpf">CPF do aluno.</param>
    [HttpGet()]
    [Route("cpf/{alunoCpf}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ObterAlunoPorCpf(string alunoCpf)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var result = await _alunoService.ObterPorIdComCpf(alunoCpf);
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
    /// Cadastra um novo aluno.
    /// </summary>
    /// <param name="aluno">Dados do aluno a ser cadastrada.</param>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> InserirAluno([FromBody] AlunoInserirDto aluno)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var result = await _alunoService.InserirAluno(aluno);
            if (result != null)
            {
                return Created();
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

    /// <summary>
    /// Atualiza um aluno.
    /// </summary>
    /// <param name="aluno">Dados do aluno a ser atualizado.</param>
    /// <remarks>
    /// Retorna um objeto com as informações sobre o aluno atualizado.
    /// </remarks>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AtualizarAluno([FromBody] AlunoAtualizarDto aluno)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var result = await _alunoService.AtualizarAluno(aluno);
            if (result != null)
            {
                return Ok(result);
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

    /// <summary>
    /// Exclui um aluno.
    /// </summary>
    /// <param name="alunoId">Identificador do aluno.</param>
    /// <remarks>
    /// Retorna um um booleano representando a exclusão do aluno.
    /// </remarks>
    [HttpDelete("{alunoId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ExcluirAluno(int alunoId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var result = await _alunoService.ExcluirAluno(alunoId);
            if (result)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }
        catch (ArgumentException e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
        }
    }
}
