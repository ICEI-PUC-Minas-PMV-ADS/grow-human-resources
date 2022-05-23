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
    public class FuncionarioService : IFuncionarioService
    {
        private readonly IGlobalPersistence _globalPersistence;
        private readonly IFuncionarioPersistence _funcionarioPersistence;
        private readonly IMapper _mapper;

        public FuncionarioService(
            IGlobalPersistence globalPersistence, 
            IFuncionarioPersistence funcionarioPersistence,
            IMapper mapper)
        {
            _globalPersistence = globalPersistence;
            _funcionarioPersistence = funcionarioPersistence;
            _mapper = mapper;
        }
        public async Task<FuncionarioDto> CriarFuncionarios(int userId, string visao, FuncionarioDto model)
        {
            try
            {
                var funcionario = _mapper.Map<Funcionario>(model);

                _globalPersistence.Cadastrar<Funcionario>(funcionario);
                if (await _globalPersistence.SalvarAsync())
                {
                    var funcionarioRetorno = await _funcionarioPersistence
                        .RecuperarFuncionarioPorIdAsync( userId,  visao, funcionario.Id, false);

                    return _mapper.Map<FuncionarioDto>(funcionarioRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<FuncionarioDto> AlterarFuncionario(int userId, string visao, int funcionarioId, FuncionarioDto model)
        {
            try
            {
                var funcionario = await _funcionarioPersistence
                    .RecuperarFuncionarioPorIdAsync( userId,  visao, funcionarioId, false);

                if (funcionario == null) return null;

                model.Id = funcionario.Id;

                _mapper.Map(model, funcionario);

                _globalPersistence.Alterar<Funcionario>(funcionario);

                if (await _globalPersistence.SalvarAsync())
                {
                    var funcionarioRetorno = await _funcionarioPersistence
                        .RecuperarFuncionarioPorIdAsync( userId,  visao, funcionario.Id, false);

                    return _mapper.Map<FuncionarioDto>(funcionarioRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> ExcluirFuncionario(int userId, string visao, int funcionarioId)
        {
            try
            {
                var funcionario = await _funcionarioPersistence
                    .RecuperarFuncionarioPorIdAsync( userId,  visao, funcionarioId, false);

                if (funcionario == null) throw new Exception("Funcionário não encontrado para exclusão");
                

                _globalPersistence.Excluir<Funcionario>(funcionario);

                return await _globalPersistence.SalvarAsync();
            }

            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        } 

        public async Task<FuncionarioDto[]> RecuperarFuncionariosAsync(int userId, string visao, bool incluirMetas = false)
        {
            try
            {
                var funcionarios = await _funcionarioPersistence
                    .RecuperarFuncionariosAsync(userId, visao, incluirMetas);

                if (funcionarios == null) return null;

                var funcionariosMapper = _mapper.Map<FuncionarioDto[]>(funcionarios);

                return funcionariosMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<FuncionarioDto[]> RecuperarFuncionariosPorNomeCompletoAsync(int userId, string visao, string nome, bool incluirMetas = false)
        {
            try
            {
                var funcionarios = await _funcionarioPersistence
                    .RecuperarFuncionariosPorNomeCompletoAsync( userId,  visao, nome, incluirMetas);

                if (funcionarios == null) return null;

                var funcionariosMapper = _mapper.Map<FuncionarioDto[]>(funcionarios);

                return funcionariosMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<FuncionarioDto> RecuperarFuncionarioPorIdAsync(int userId, string visao, int funcionarioId, bool incluirMetas = false)
        {
            try
            {
                var funcionario = await _funcionarioPersistence
                    .RecuperarFuncionarioPorIdAsync( userId,  visao, funcionarioId, incluirMetas);

                if (funcionario == null) return null;

                var funcionarioMapper = _mapper.Map<FuncionarioDto>(funcionario);

                return funcionarioMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<FuncionarioDto> RecuperarFuncionarioPorContaIdAsync(int contaId)
        {
            try
            {
                var funcionario = await _funcionarioPersistence
                    .RecuperarFuncionarioPorContaIdAsync(contaId);

                if (funcionario == null) return null;

                var funcionarioMapper = _mapper.Map<FuncionarioDto>(funcionario);

                return funcionarioMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}