

using System;
using System.Threading.Tasks;
using AutoMapper;
using GHR.Application.Dtos.Funcionarios;
using GHR.Application.Services.Contracts.Funcionarios;
using GHR.Domain.DataBase.Funcionarios;
using GHR.Persistence.Interfaces.Contracts.Funcionarios;
using GHR.Persistence.Interfaces.Contracts.Global;

namespace GHR.Application.Services.Implements.Funcionarios
{
    public class FuncionarioDadoPessoalService : IFuncionarioDadoPessoalService
    {
        private readonly IGlobalPersistence _globalPersistence;
        private readonly IFuncionarioDadoPessoalPersistence _funcionarioDadoPessoalPersistence;
        private readonly IMapper _mapper;

        public FuncionarioDadoPessoalService(
            IGlobalPersistence globalPersistence, 
            IFuncionarioDadoPessoalPersistence funcionarioDadoPessoalPersistence,
            IMapper mapper)
        {
            _globalPersistence = globalPersistence;
            _funcionarioDadoPessoalPersistence = funcionarioDadoPessoalPersistence;
            _mapper = mapper;
        }
        public async Task<DadoPessoalDto> CriarDadoPessoalAsync(DadoPessoalDto model)
        {
            try
            {
                var dadoPessoal = _mapper.Map<DadoPessoal>(model);

                _globalPersistence.Cadastrar<DadoPessoal>(dadoPessoal);

                if (await _globalPersistence.SalvarAsync())
                {
                    var dadoPessoalRetorno = await _funcionarioDadoPessoalPersistence
                        .RecuperarDadosPessoaisPorIdAsync(dadoPessoal.Id, dadoPessoal.EmpresaId, dadoPessoal.FuncionarioId);

                    return _mapper.Map<DadoPessoalDto>(dadoPessoalRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<DadoPessoalDto> AlterarDadoPessoalAsync(int dadoPessoalId, DadoPessoalDto model)
        {
            try
            {
                var dadoPessoal = await _funcionarioDadoPessoalPersistence
                    .RecuperarDadosPessoaisPorIdAsync(dadoPessoalId, model.EmpresaId, model.FuncionarioId);

                if (dadoPessoal == null) return null;

                model.Id = dadoPessoal.Id;
                
                _mapper.Map(model, dadoPessoal);

                _globalPersistence.Alterar<DadoPessoal>(dadoPessoal);

                if (await _globalPersistence.SalvarAsync())
                {
                    var dadoPessoalRetorno = await _funcionarioDadoPessoalPersistence
                        .RecuperarDadosPessoaisPorIdAsync( dadoPessoal.Id,  model.EmpresaId, model.FuncionarioId);

                    return _mapper.Map<DadoPessoalDto>(dadoPessoalRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> ExcluirDadoPessoalAsync(int dadoPessoalId, int empresaId, int funcionarioId)
        {
            try
            {
                var dadoPessoal = await _funcionarioDadoPessoalPersistence
                    .RecuperarDadosPessoaisPorIdAsync(dadoPessoalId, empresaId, funcionarioId);

                if (dadoPessoal == null) throw new Exception("Dado Pessoal não encontrado para exclusão");
                

                _globalPersistence.Excluir<DadoPessoal>(dadoPessoal);

                return await _globalPersistence.SalvarAsync();
            }

            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

           } 

        public async Task<DadoPessoalDto> RecuperarDadoPessoalPorIdAsync(int dadoPessoalId, int empresaId, int funcionarioId)
        {
            try
            {
                var dadoPessoal = await _funcionarioDadoPessoalPersistence
                    .RecuperarDadosPessoaisPorIdAsync(dadoPessoalId, empresaId, funcionarioId);

                if (dadoPessoal == null) return null;

                var dadoPessoalMapper = _mapper.Map<DadoPessoalDto>(dadoPessoal);

                return dadoPessoalMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

    }
}