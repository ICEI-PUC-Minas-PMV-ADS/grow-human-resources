using System;
using System.Threading.Tasks;
using AutoMapper;
using GHR.Application.Dtos.Cargos;
using GHR.Application.Services.Contracts.Cargos;
using GHR.Domain.DataBase.Cargos;
using GHR.Persistence.Interfaces.Contracts.Cargos;
using GHR.Persistence.Interfaces.Contracts.Global;

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
                        .RecuperarCargoPorIdAsync(cargo.Id);

                    return _mapper.Map<CargoDto>(cargoRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> ExcluirCargo(int cargoId)
        {
            try
            {
                var cargo = await _cargoPersistence.RecuperarCargoPorIdAsync(cargoId);

                if (cargo == null) throw new Exception("Cargo não encontrado para exclusão");


                _globalPersistence.Excluir<Cargo>(cargo);

                return await _globalPersistence.SalvarAsync();
            }

            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<CargoDto[]> RecuperarCargosAsync()
        {
            try
            {
                var cargos = await _cargoPersistence
                    .RecuperarCargosAsync();

                if (cargos == null) return null;

                var cargosMapper = _mapper.Map<CargoDto[]>(cargos);

                return cargosMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<CargoDto[]> RecuperarCargosPorNomeCargoAsync(string nome)
        {
            try
            {
                var cargos = await _cargoPersistence
                    .RecuperarCargosPorNomeCargoAsync(nome);

                if (cargos == null) return null;

                var cargosMapper = _mapper.Map<CargoDto[]>(cargos);

                return cargosMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<CargoDto> RecuperarCargoPorIdAsync(int cargoId)
        {
            try
            {
                var cargo = await _cargoPersistence
                    .RecuperarCargoPorIdAsync(cargoId);

                if (cargo == null) return null;

                var cargoMapper = _mapper.Map<CargoDto>(cargo);

                return cargoMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<CargoDto> AlterarCargo(int cargoId, CargoDto model)
        {
            try
            {
                var cargo = await _cargoPersistence
                    .RecuperarCargoPorIdAsync( cargoId);

                if (cargo == null) return null;

                model.Id = cargo.Id;

                _mapper.Map(model, cargo);

                _globalPersistence.Alterar<Cargo>(cargo);

                if (await _globalPersistence.SalvarAsync())
                {
                    var cargoRetorno = await _cargoPersistence
                        .RecuperarCargoPorIdAsync(cargo.Id);

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