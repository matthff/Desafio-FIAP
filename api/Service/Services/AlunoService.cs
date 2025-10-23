using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.DTO.Aluno;
using Api.Domain.Entidades;
using Api.Domain.Interfaces.Repository;
using Api.Domain.Interfaces.Services;
using AutoMapper;
using Domain.Utils;

namespace Api.Service.Services;

public class AlunoService : BaseService<Aluno, AlunoDto>, IAlunoService
{
    private readonly IAlunoRepository _alunoRepository;
    private readonly ISenhaService<Aluno> _senhaService;

    public AlunoService(IAlunoRepository alunoRepository, IMapper mapper, ISenhaService<Aluno> senhaService) : base(alunoRepository, mapper)
    {
        _alunoRepository = alunoRepository;
        _senhaService = senhaService;
    }

    public async Task<PagedResult<AlunoDto>> ObterTodosOrdenadosPorNomeAsync(
        int page = Pagination.DefaultPageNumber,
        int pageSize = Pagination.DefaultPageSize)
    {
        var listEntity = await _alunoRepository.ObterTodosAsync();

        var totalCount = listEntity.Count();
        var items = listEntity
            .OrderBy(a => a.Nome)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new PagedResult<AlunoDto>
        {
            Items = _mapper.Map<IEnumerable<AlunoDto>>(items).ToList(),
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }

    public async Task<AlunoDto> ObterPorIdComNomeAsync(string alunoNome)
    {
        return _mapper.Map<AlunoDto>(await _alunoRepository.ObterPorIdComNomeAsync(alunoNome));
    }

    public async Task<AlunoDto> ObterPorIdComCpfAsync(string alunoCpf)
    {
        return _mapper.Map<AlunoDto>(await _alunoRepository.ObterPorIdComCpfAsync(alunoCpf));
    }

    public async Task<AlunoDto> InserirAlunoAsync(AlunoInserirDto alunoCriado)
    {
        var entity = _mapper.Map<Aluno>(alunoCriado);

        if (await _alunoRepository.ExisteAlunoAsync(entity))
            return null;

        entity.DefinirSenha(_senhaService.DefinirHashDaSenha(entity, alunoCriado.Senha));

        var result = await _alunoRepository.InserirAsync(entity);

        return _mapper.Map<AlunoDto>(result);
    }

    public async Task<AlunoDto> AtualizarAlunoAsync(AlunoAtualizarDto alunoAtualizado)
    {
        var entity = _mapper.Map<Aluno>(alunoAtualizado);

        if (await _alunoRepository.ExisteAlunoAsync(entity))
            return null;

        entity.DefinirSenha(_senhaService.DefinirHashDaSenha(entity, alunoAtualizado.Senha));

        var result = await _alunoRepository.AtualizarParcialAsync(entity,
            e => e.Nome,
            e => e.Email,
            e => e.SenhaHash,
            e => e.Cpf,
            e => e.DataNascimento);

        return _mapper.Map<AlunoDto>(result);
    }

    public async Task<bool> ExcluirAlunoAsync(int alunoId)
    {
        return await _alunoRepository.ExcluirAsync(alunoId);
    }
}
