using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GHR.Application.Dtos.Funcionarios;
using GHR.Application.Services.Contracts.Funcionarios;
using GHR.Domain.DataBase.Funcionarios;
using GHR.Persistence.Interfaces.Contracts.Funcionarios;
using GHR.Persistence.Interfaces.Contracts.Global;
using GHR.Persistence.Models;

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
        public async Task<FuncionarioDto> CriarFuncionarios(FuncionarioDto model)
        {
            try
            {
                var funcionario = _mapper.Map<Funcionario>(model);

                _globalPersistence.Cadastrar<Funcionario>(funcionario);
                if (await _globalPersistence.SalvarAsync())
                {
                    var funcionarioRetorno = await _funcionarioPersistence
                        .RecuperarFuncionarioPorIdAsync(funcionario.Id, funcionario.EmpresaId, false);

                    return _mapper.Map<FuncionarioDto>(funcionarioRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<FuncionarioDto> AlterarFuncionario(int funcionarioId, int empresaId, FuncionarioDto model)
        {
            try
            {
                var funcionario = await _funcionarioPersistence
                    .RecuperarFuncionarioPorIdAsync(funcionarioId, empresaId, false);

                if (funcionario == null) return null;

                model.Id = funcionario.Id;

                _mapper.Map(model, funcionario);

                _globalPersistence.Alterar<Funcionario>(funcionario);

                if (await _globalPersistence.SalvarAsync())
                {
                    var funcionarioRetorno = await _funcionarioPersistence
                        .RecuperarFuncionarioPorIdAsync( funcionario.Id, empresaId, false);

                    return _mapper.Map<FuncionarioDto>(funcionarioRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> ExcluirFuncionario( int funcionarioId, int empresaId)
        {
            try
            {
                var funcionario = await _funcionarioPersistence
                    .RecuperarFuncionarioPorIdAsync( funcionarioId, empresaId, false);

                if (funcionario == null) throw new Exception("Funcionário não encontrado para exclusão");
                

                _globalPersistence.Excluir<Funcionario>(funcionario);

                return await _globalPersistence.SalvarAsync();
            }

            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        } 

        public async Task<PaginaLista<FuncionarioDto>> RecuperarFuncionariosAsync(int empresaId, PaginaParametros paginaParametros, bool incluirMetas = false)
        {
            try
            {
                var funcionarios = await _funcionarioPersistence
                    .RecuperarFuncionariosAsync(empresaId, paginaParametros, incluirMetas);

                if (funcionarios == null) return null;

                var funcionariosMapper = _mapper.Map<PaginaLista<FuncionarioDto>>(funcionarios);

                funcionariosMapper.PaginaAtual = funcionarios.PaginaAtual;
                funcionariosMapper.TotalDePaginas = funcionarios.TotalDePaginas;
                funcionariosMapper.TamanhoDaPagina = funcionarios.TamanhoDaPagina;
                funcionariosMapper.ContadorTotal = funcionarios.ContadorTotal;

                return funcionariosMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<FuncionarioDto> RecuperarFuncionarioPorIdAsync(int funcionarioId, int empresaId, bool incluirMetas = false)
        {
            try
            {
                var funcionario = await _funcionarioPersistence
                    .RecuperarFuncionarioPorIdAsync( funcionarioId, empresaId, incluirMetas);

                if (funcionario == null) return null;

                var funcionarioMapper = _mapper.Map<FuncionarioDto>(funcionario);

                return funcionarioMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<FuncionarioDto> RecuperarFuncionarioPorContaIdAsync(int contaId, int empresaId)
        {
            try
            {
                var funcionario = await _funcionarioPersistence
                    .RecuperarFuncionarioPorContaIdAsync(contaId, empresaId);

                if (funcionario == null) return null;

                var funcionarioMapper = _mapper.Map<FuncionarioDto>(funcionario);

                return funcionarioMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<FuncionarioDto[]> RecuperarFuncionarioPorDepartamentoIdAsync(int departamentoId, int empresaId)
        {
            try
            {
                var funcionarios = await _funcionarioPersistence
                    .RecuperarFuncionarioPorDepartamentoIdAsync(departamentoId, empresaId);

                if (funcionarios == null) return null;

                var funcionariosMapper = _mapper.Map<FuncionarioDto[]>(funcionarios);

                return funcionariosMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}