using System;
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
using Microsoft.AspNetCore.Identity;

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

    public async Task<Administrador> ObterAdministradorPorEmail(string email)
    {
        return await _administradorRepository.ObterAdministradorPorEmail(email); ;
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

    public async Task<Administrador> ValidarCredenciaisDoAdministrador(AdministradorLoginDto administradorLoginDto)
    {
        var entity = await _administradorRepository.ObterAdministradorPorEmail(administradorLoginDto.Email);

        if (entity == null)
            return null;

        if (!_senhaService.VerificarSenha(entity, administradorLoginDto.Senha))
            return null;

        return entity;
    }

    public async Task RecarregarInformacoesDoAdministrador(Administrador administrador)
    {
        await _administradorRepository.RecarregarInformacoesDoAdministrador(administrador);
    }

    public async Task<bool> RevogarToken(string email)
    {
        return await _administradorRepository.RevogarToken(email);
    }
}
