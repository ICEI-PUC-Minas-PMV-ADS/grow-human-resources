using System;
using System.Threading.Tasks;
using AutoMapper;
using GHR.Application.Dtos.Departamentos;
using GHR.Application.Services.Contracts.Departamentos;
using GHR.Domain.DataBase.Departamentos;
using GHR.Persistence.Interfaces.Contracts.Departamentos;
using GHR.Persistence.Interfaces.Contracts.Global;

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
                        .RecuperarDepartamentoPorIdAsync(departamento.Id);

                    return _mapper.Map<DepartamentoDto>(departamentoRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> ExcluirDepartamento( int departamentoId)
        {
            try
            {
                var departamento = await _departamentoPersistence
                    .RecuperarDepartamentoPorIdAsync(departamentoId);

                if (departamento == null) throw new Exception("Departamento não encontrado para exclusão");


                _globalPersistence.Excluir<Departamento>(departamento);

                return await _globalPersistence.SalvarAsync();
            }

            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<DepartamentoDto[]> RecuperarDepartamentosAsync()
        {
            try
            {
                var departamentos = await _departamentoPersistence
                    .RecuperarDepartamentosAsync();

                if (departamentos == null) return null;

                var departamentosMapper = _mapper.Map<DepartamentoDto[]>(departamentos);

                return departamentosMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<DepartamentoDto[]> RecuperarDepartamentosPorNomeDepartamentoAsync(string nome)
        {
            try
            {
                var departamentos = await _departamentoPersistence
                    .RecuperarDepartamentosPorNomeDepartamentoAsync(nome);

                if (departamentos == null) return null;

                var departamentosMapper = _mapper.Map<DepartamentoDto[]>(departamentos);

                return departamentosMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<DepartamentoDto> RecuperarDepartamentoPorIdAsync(int departamentoId)
        {
            try
            {
                var departamento = await _departamentoPersistence
                    .RecuperarDepartamentoPorIdAsync(departamentoId);

                if (departamento == null) return null;

                var departamentoMapper = _mapper.Map<DepartamentoDto>(departamento);

                return departamentoMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<DepartamentoDto> AlterarDepartamento(int departamentoId, DepartamentoDto model)
        {
            try
            {
                var departamento = await _departamentoPersistence
                    .RecuperarDepartamentoPorIdAsync( departamentoId);

                if (departamento == null) return null;

                model.Id = departamento.Id;

                _mapper.Map(model, departamento);

                _globalPersistence.Alterar<Departamento>(departamento);

                if (await _globalPersistence.SalvarAsync())
                {
                    var departamentoRetorno = await _departamentoPersistence
                        .RecuperarDepartamentoPorIdAsync(departamento.Id);

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