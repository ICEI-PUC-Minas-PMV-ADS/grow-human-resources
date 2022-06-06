using System;
using System.Threading.Tasks;
using AutoMapper;
using GHR.Application.Dtos.Departamentos;
using GHR.Application.Services.Contracts.Departamentos;
using GHR.Domain.DataBase.Departamentos;
using GHR.Persistence.Interfaces.Contracts.Departamentos;
using GHR.Persistence.Interfaces.Contracts.Global;
using GHR.Persistence.Models;

namespace GHR.Application.Services.Implements.Departamentos
{
    public class DepartamentoService : IDepartamentoService
    {
        private readonly IGlobalPersistence _globalPersistence;
        private readonly IDepartamentoPersistence _departamentoPersistence;
        private readonly IMapper _mapper;

        public DepartamentoService(
            IGlobalPersistence globalPersistence,
            IDepartamentoPersistence DepartamentoPersistence,
            IMapper mapper)
        {
            _globalPersistence = globalPersistence;
            _departamentoPersistence = DepartamentoPersistence;
            _mapper = mapper;
        }
        public async Task<DepartamentoDto> CriarDepartamento(DepartamentoDto model)
        {
            try
            {
                var departamento = _mapper.Map<Departamento>(model);

                _globalPersistence.Cadastrar<Departamento>(departamento);
                
                if (await _globalPersistence.SalvarAsync())
                {
                    var departamentoRetorno = await _departamentoPersistence
                        .RecuperarDepartamentoPorIdAsync(departamento.Id, departamento.EmpresaId);

                    return _mapper.Map<DepartamentoDto>(departamentoRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> ExcluirDepartamento( int departamentoId, int empresaId)
        {
            try
            {
                var departamento = await _departamentoPersistence
                    .RecuperarDepartamentoPorIdAsync(departamentoId, empresaId);

                if (departamento == null) throw new Exception("Departamento não encontrado para exclusão");


                _globalPersistence.Excluir<Departamento>(departamento);

                return await _globalPersistence.SalvarAsync();
            }

            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<PaginaLista<DepartamentoDto>> RecuperarDepartamentosAsync(int empresaId, PaginaParametros paginaParametros)
        {
            try
            {
                var departamentos = await _departamentoPersistence
                    .RecuperarDepartamentosAsync(paginaParametros, empresaId);

                if (departamentos == null) return null;

                var departamentosMapper = _mapper.Map<PaginaLista<DepartamentoDto>>(departamentos);

                departamentosMapper.PaginaAtual = departamentos.PaginaAtual;
                departamentosMapper.TotalDePaginas = departamentos.TotalDePaginas;
                departamentosMapper.TamanhoDaPagina = departamentos.TamanhoDaPagina;
                departamentosMapper.ContadorTotal = departamentos.ContadorTotal;

                return departamentosMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<DepartamentoDto> RecuperarDepartamentoPorIdAsync(int departamentoId, int empresaId)
        {
            try
            {
                var departamento = await _departamentoPersistence
                    .RecuperarDepartamentoPorIdAsync(departamentoId, empresaId);

                if (departamento == null) return null;

                var departamentoMapper = _mapper.Map<DepartamentoDto>(departamento);

                return departamentoMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<DepartamentoDto> AlterarDepartamento(int departamentoId, int empresaId, DepartamentoDto model)
        {
            try
            {
                var departamento = await _departamentoPersistence
                    .RecuperarDepartamentoPorIdAsync( departamentoId, empresaId);

                if (departamento == null) return null;

                model.Id = departamento.Id;

                _mapper.Map(model, departamento);

                _globalPersistence.Alterar<Departamento>(departamento);

                if (await _globalPersistence.SalvarAsync())
                {
                    var departamentoRetorno = await _departamentoPersistence
                        .RecuperarDepartamentoPorIdAsync(departamento.Id, departamento.EmpresaId);

                    return _mapper.Map<DepartamentoDto>(departamentoRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}