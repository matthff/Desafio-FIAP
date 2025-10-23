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

    public async Task<Administrador> ObterAdministradorPorEmailAsync(string email)
    {
        return await _administradorRepository.ObterAdministradorPorEmailAsync(email); ;
    }

    public async Task<AdministradorDto> InserirAdministradorAsync(AdministradorInserirDto administradorCriado)
    {
        var entity = _mapper.Map<Administrador>(administradorCriado);

        if (await _administradorRepository.ExisteAdministradorComMesmoEmailAsync(entity))
            return null;

        entity.DefinirSenha(_senhaService.DefinirHashDaSenha(entity, administradorCriado.Senha));

        var result = await _administradorRepository.InserirAsync(entity);

        return _mapper.Map<AdministradorDto>(result);
    }

    public async Task<AdministradorDto> AtualizarAdministradorAsync(AdministradorAtualizarDto administradorAtualizado)
    {
        var entity = _mapper.Map<Administrador>(administradorAtualizado);

        if (await _administradorRepository.ExisteAdministradorComMesmoEmailAsync(entity))
            return null;

        var result = await _administradorRepository.AtualizarParcialAsync(entity,
            e => e.Nome,
            e => e.Email);

        return _mapper.Map<AdministradorDto>(result);
    }

    public async Task<Administrador> ValidarCredenciaisDoAdministradorAsync(AdministradorLoginDto administradorLoginDto)
    {
        var entity = await _administradorRepository.ObterAdministradorPorEmailAsync(administradorLoginDto.Email);

        if (entity == null)
            return null;

        if (!_senhaService.ValidarSenha(entity, administradorLoginDto.Senha))
            return null;

        return entity;
    }

    public async Task RecarregarInformacoesDoAdministradorAsync(Administrador administrador)
    {
        await _administradorRepository.RecarregarInformacoesDoAdministradorAsync(administrador);
    }

    public async Task<bool> RevogarTokenAsync(string email)
    {
        return await _administradorRepository.RevogarTokenAsync(email);
    }
}
