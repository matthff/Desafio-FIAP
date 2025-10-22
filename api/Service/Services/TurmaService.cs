using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Api.Domain.DTO.Turma;
using Api.Domain.Entidades;
using Api.Domain.Interfaces.Repository;
using Api.Domain.Interfaces.Services;
using AutoMapper;
using System;

namespace Api.Service.Services
{
    public class TurmaService : BaseService<Turma, TurmaDto>, ITurmaService
    {
        private readonly ITurmaRepository _turmaRepository;

        public TurmaService(ITurmaRepository turmaRepository, IMapper mapper) : base(turmaRepository, mapper)
        {
            _turmaRepository = turmaRepository;
        }

        public async Task<IEnumerable<TurmaDto>> ObterTodosComQuantidadeDeAlunos()
        {
            var listEntity = await _turmaRepository.ObterTodosComAlunos();
            return _mapper.Map<IEnumerable<TurmaDto>>(listEntity.OrderBy(t => t.Nome));
        }

        public async Task<TurmaDto> ObterPorIdComQuantidadeDeAlunos(int turmaId)
        {
            return _mapper.Map<TurmaDto>(await _turmaRepository.ObterPorIdComAlunos(turmaId));
        }

        public async Task<TurmaDto> InserirTurma(TurmaInserirDto turmaCriada)
        {
            var entity = _mapper.Map<Turma>(turmaCriada);
            var result = await _turmaRepository.InserirAsync(entity);

            return _mapper.Map<TurmaDto>(result);
        }

        public async Task<TurmaDto> AtualizarTurma(TurmaAtualizarDto turmaAtualizada)
        {
            var entity = _mapper.Map<Turma>(turmaAtualizada);
            var result = await _turmaRepository.AtualizarParcialAsync(entity,
                                                                    e => e.Nome,
                                                                    e => e.Descricao);
            return _mapper.Map<TurmaDto>(result);
        }

        public async Task<bool> ExcluirTurma(int turmaId)
        {
            return await _turmaRepository.ExcluirAsync(turmaId);
        }
    }
}
