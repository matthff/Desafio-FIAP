using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.DTO.Aluno;
using Api.Domain.Entidades;
using Api.Domain.Interfaces.Repository;
using Api.Domain.Interfaces.Services;
using AutoMapper;

namespace Api.Service.Services;

public class AlunoService : BaseService<Aluno, AlunoDto>, IAlunoService
{
    private readonly IAlunoRepository _alunoRepository;

    public AlunoService(IAlunoRepository alunoRepository, IMapper mapper) : base(alunoRepository, mapper)
    {
        _alunoRepository = alunoRepository;
    }

    public async Task<IEnumerable<AlunoDto>> ObterTodosAlunosOrdenadosPorNome()
    {
        var listEntity = await _alunoRepository.ObterTodosAsync();
        return _mapper.Map<IEnumerable<AlunoDto>>(listEntity.OrderBy(a => a.Nome));
    }

    public async Task<AlunoDto> ObterPorIdComNome(string alunoNome)
    {
        return _mapper.Map<AlunoDto>(await _alunoRepository.ObterPorIdComNome(alunoNome));
    }

    public async Task<AlunoDto> ObterPorIdComCpf(string alunoCpf)
    {
        return _mapper.Map<AlunoDto>(await _alunoRepository.ObterPorIdComCpf(alunoCpf));
    }

    public async Task<AlunoDto> InserirAluno(AlunoInserirDto alunoCriado)
    {
        var entity = _mapper.Map<Aluno>(alunoCriado);

        if (await _alunoRepository.ExisteAluno(entity))
            return null;

        var result = await _alunoRepository.InserirAsync(entity);

        return _mapper.Map<AlunoDto>(result);
    }

    public async Task<AlunoDto> AtualizarAluno(AlunoAtualizarDto alunoAtualizado)
    {
        var entity = _mapper.Map<Aluno>(alunoAtualizado);

        if (await _alunoRepository.ExisteAluno(entity))
            return null;

        var result = await _alunoRepository.AtualizarParcialAsync(entity,
            e => e.Nome,
            e => e.Email,
            e => e.SenhaHash,
            e => e.Cpf,
            e => e.DataNascimento);

        return _mapper.Map<AlunoDto>(result);
    }

    public async Task<bool> ExcluirAluno(int alunoId)
    {
        return await _alunoRepository.ExcluirAsync(alunoId);
    }
}
