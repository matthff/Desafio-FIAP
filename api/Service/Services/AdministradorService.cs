using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.DTO.Administrador;
using Api.Domain.DTO.Aluno;
using Api.Domain.Entidades;
using Api.Domain.Interfaces.Repository;
using Api.Domain.Interfaces.Services;
using AutoMapper;
using Domain.Utils;

namespace Api.Service.Services;

public class AdministradorService : IAdministradorService
{
    private readonly IAdministradorRepository _administradorRepository;
    private readonly IMapper _mapper;
    private readonly ISenhaService<Administrador> _senhaService;

    public AdministradorService(IAdministradorRepository administradorRepository, IMapper mapper, ISenhaService<Administrador> senhaService)
    {
        _administradorRepository = administradorRepository;
        _mapper = mapper;
        _senhaService = senhaService;
    }

    public async Task<AdministradorDto> InserirAdministrador(AdministradorInserirDto administradorCriado)
    {
        var entity = _mapper.Map<Administrador>(administradorCriado);

        if (await _administradorRepository.ExisteAdministradorComMesmoEmailAsync(entity))
            return null;

        entity.DefinirSenha(_senhaService.HashSenha(entity, administradorCriado.Senha));

        var result = await _administradorRepository.InserirAsync(entity);

        return _mapper.Map<AdministradorDto>(result);
    }

    public async Task<AdministradorDto> AtualizarAdministrador(AdministradorAtualizarDto administradorAtualizado)
    {
        var entity = _mapper.Map<Administrador>(administradorAtualizado);

        if (await _administradorRepository.ExisteAdministradorComMesmoEmailAsync(entity))
            return null;

        var result = await _administradorRepository.AtualizarParcialAsync(entity,
            e => e.Nome,
            e => e.Email);

        return _mapper.Map<AdministradorDto>(result);
    }
}
