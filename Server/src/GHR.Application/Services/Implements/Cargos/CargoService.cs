using System;
using System.Threading.Tasks;
using AutoMapper;
using GHR.Application.Dtos.Cargos;
using GHR.Application.Services.Contracts.Cargos;
using GHR.Domain.DataBase.Cargos;
using GHR.Persistence.Interfaces.Contracts.Cargos;
using GHR.Persistence.Interfaces.Contracts.Global;
using GHR.Persistence.Models;

namespace GHR.Application.Services.Implements.Cargos
{
    public class CargoService : ICargoService
    {
        private readonly IGlobalPersistence _globalPersistence;
        private readonly ICargoPersistence _cargoPersistence;
        private readonly IMapper _mapper;

        public CargoService(
            IGlobalPersistence globalPersistence,
            ICargoPersistence CargoPersistence,
            IMapper mapper)
        {
            _globalPersistence = globalPersistence;
            _cargoPersistence = CargoPersistence;
            _mapper = mapper;
        }
        public async Task<CargoDto> CriarCargo(CargoDto model)
        {
            try
            {
                var cargo = _mapper.Map<Cargo>(model);

                _globalPersistence.Cadastrar<Cargo>(cargo);
                
                if (await _globalPersistence.SalvarAsync())
                {
                    var cargoRetorno = await _cargoPersistence
                        .RecuperarCargoPorIdAsync(cargo.Id, cargo.EmpresaId);

                    return _mapper.Map<CargoDto>(cargoRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> ExcluirCargo(int cargoId, int empresaId)
        {
            try
            {
                var cargo = await _cargoPersistence.RecuperarCargoPorIdAsync(cargoId, empresaId);

                if (cargo == null) throw new Exception("Cargo não encontrado para exclusão");


                _globalPersistence.Excluir<Cargo>(cargo);

                return await _globalPersistence.SalvarAsync();
            }

            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<PaginaLista<CargoDto>> RecuperarCargosAsync(int empresaId, PaginaParametros paginaParametros)
        {
            try
            {
                var cargos = await _cargoPersistence
                    .RecuperarCargosAsync(paginaParametros, empresaId);

                if (cargos == null) return null;

                var cargosMapper = _mapper.Map<PaginaLista<CargoDto>>(cargos);

                cargosMapper.PaginaAtual = cargos.PaginaAtual;
                cargosMapper.TotalDePaginas = cargos.TotalDePaginas;
                cargosMapper.TamanhoDaPagina = cargos.TamanhoDaPagina;
                cargosMapper.ContadorTotal = cargos.ContadorTotal;

                return cargosMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<CargoDto> RecuperarCargoPorIdAsync(int cargoId, int empresaId)
        {
            try
            {
                var cargo = await _cargoPersistence
                    .RecuperarCargoPorIdAsync(cargoId, empresaId);

                if (cargo == null) return null;

                var cargoMapper = _mapper.Map<CargoDto>(cargo);

                return cargoMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<CargoDto[]> RecuperarCargosPorDepartamentoIdAsync(int departamentoId, int empresaId)
        {
            try
            {
                var cargo = await _cargoPersistence
                    .RecuperarCargosPorDepartamentoIdAsync(departamentoId, empresaId);

                if (cargo == null) return null;

                var cargoMapper = _mapper.Map<CargoDto[]>(cargo);

                return cargoMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<CargoDto> AlterarCargo(int cargoId, int empresaId, CargoDto model)
        {
            try
            {
                var cargo = await _cargoPersistence
                    .RecuperarCargoPorIdAsync( cargoId, empresaId);

                if (cargo == null) return null;

                model.Id = cargo.Id;

                _mapper.Map(model, cargo);

                _globalPersistence.Alterar<Cargo>(cargo);

                if (await _globalPersistence.SalvarAsync())
                {
                    var cargoRetorno = await _cargoPersistence
                        .RecuperarCargoPorIdAsync(cargo.Id, cargo.EmpresaId);

                    return _mapper.Map<CargoDto>(cargoRetorno);
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