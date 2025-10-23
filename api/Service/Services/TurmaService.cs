using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Api.Domain.DTO.Turma;
using Api.Domain.Entidades;
using Api.Domain.Interfaces.Repository;
using Api.Domain.Interfaces.Services;
using AutoMapper;
using System;
using Domain.Utils;

namespace Api.Service.Services
{
    public class TurmaService : BaseService<Turma, TurmaDto>, ITurmaService
    {
        private readonly ITurmaRepository _turmaRepository;

        public TurmaService(ITurmaRepository turmaRepository, IMapper mapper) : base(turmaRepository, mapper)
        {
            _turmaRepository = turmaRepository;
        }

        public async Task<PagedResult<TurmaDto>> ObterTurmasOrdenadasPorNomeComQuantidadeDeAlunosAsync(
            int page = Pagination.DefaultPageNumber,
            int pageSize = Pagination.DefaultPageSize)
        {
            var listEntity = await _turmaRepository.ObterTodosComAlunosAsync();

            var totalCount = listEntity.Count();
            var items = listEntity
                .OrderBy(a => a.Nome)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedResult<TurmaDto>
            {
                Items = _mapper.Map<IEnumerable<TurmaDto>>(items).ToList(),
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<TurmaDto> ObterPorIdComQuantidadeDeAlunosAsync(int turmaId)
        {
            return _mapper.Map<TurmaDto>(await _turmaRepository.ObterPorIdComAlunosAsync(turmaId));
        }

        public async Task<TurmaDto> InserirTurmaAsync(TurmaInserirDto turmaCriada)
        {
            var entity = _mapper.Map<Turma>(turmaCriada);
            var result = await _turmaRepository.InserirAsync(entity);

            return _mapper.Map<TurmaDto>(result);
        }

        public async Task<TurmaDto> AtualizarTurmaAsync(TurmaAtualizarDto turmaAtualizada)
        {
            var entity = _mapper.Map<Turma>(turmaAtualizada);
            var result = await _turmaRepository.AtualizarParcialAsync(entity,
                                                                    e => e.Nome,
                                                                    e => e.Descricao);
            return _mapper.Map<TurmaDto>(result);
        }

        public async Task<bool> ExcluirTurmaAsync(int turmaId)
        {
            return await _turmaRepository.ExcluirAsync(turmaId);
        }
    }
}
