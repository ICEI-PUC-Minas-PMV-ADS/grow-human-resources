using System;
using System.Collections.Generic;
using System.Threading.Tasks;


using AutoMapper;

using GHR.Application.Dtos.Empresas;
using GHR.Application.Services.Contracts.Empresas;
using GHR.Domain.DataBase.Empresas;
using GHR.Persistence.Interfaces.Contracts.Empresas;
using GHR.Persistence.Interfaces.Contracts.Global;


namespace GHR.Application.Services.Implements.Empresas
{
    public class EmpresaContaService : IEmpresaContaService
    {
        private readonly IGlobalPersistence _globalPersistence;
        private readonly IMapper _mapper;
        private readonly IEmpresaContaPersistence _empresaContaPersistence;

        public EmpresaContaService(
            IGlobalPersistence globalPersistence,
            IEmpresaContaPersistence empresaContaPersistence,
            IMapper mapper
        )
        {
            _globalPersistence = globalPersistence;
            _mapper = mapper;
            _empresaContaPersistence = empresaContaPersistence;
        }

        public async Task<EmpresaContaDto> AtualizarEmpresa(EmpresaContaDto empresaContaDto)
        {
            try
            {
                var empresa = await _empresaContaPersistence.RecuperarEmpresaPorIdAsync(empresaContaDto.EmpresaId);

                if (empresa == null) return null;
                    
               empresaContaDto.EmpresaId = empresa.Id;

                _mapper.Map(empresaContaDto, empresa);

                _empresaContaPersistence.Alterar<Empresa>(empresa);

                if (await _empresaContaPersistence.SalvarAsync()) {
                    var empresaRetorno = await _empresaContaPersistence.RecuperarEmpresaPorIdAsync(empresa.Id);

                    return _mapper.Map<EmpresaContaDto>(empresaRetorno);
                }

                return null;
            }
            catch (System.Exception ex)
            {

                throw new Exception($"Falha ao salvar empresa. Erro: {ex.Message}");
            }    
        }
        public async Task<EmpresaContaDto> CriarEmpresaContaAsync(EmpresaContaDto empresaContaDto)
        {
            try
            {
                var empresaConta = _mapper.Map<EmpresaConta>(empresaContaDto);
                
                _globalPersistence.Cadastrar<EmpresaConta>(empresaConta);

                if (await _globalPersistence.SalvarAsync()) {
                    var empresaRetorno = _mapper.Map<EmpresaContaDto>(empresaConta);
                    return empresaRetorno;
                }

                return null;
            }
            catch (System.Exception ex)
            {

                throw new Exception($"Falha ao criar Empresa/Conta. Erro: {ex.Message}");
            }
        }
    
        public async Task<EmpresaContaDto> RecuperarEmpresaContaPorEmpresaIdUserNameAsync(int empresaId, string userName)
        {
            try
            {
                var empresaConta = await _empresaContaPersistence.RecuperarEmpresaContaPorEmpresaIdUserNameAsync(empresaId, userName);

                if (empresaConta == null) return null;

                var empresaContaDto = _mapper.Map<EmpresaContaDto>(empresaConta);

                return empresaContaDto;
            }
            catch (System.Exception ex)
            {

                throw new Exception($"Falha ao recuperar Empresa/Conta. Erro: {ex.Message}");
            }
        }
    }
}