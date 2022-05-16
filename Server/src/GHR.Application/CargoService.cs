using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GHR.Application.Dtos;
using GHR.Domain;
using GHR.Persistence.Contracts;

namespace GHR.Application.Contracts
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
        public async Task<CargoDto> AddCargo(int userId, string visao, CargoDto model)
        {
            try
            {
                var cargo = _mapper.Map<Cargo>(model);

                _globalPersistence.Add<Cargo>(cargo);
                if (await _globalPersistence.SaveChangeAsync())
                {
                    var cargoRetorno = await _cargoPersistence.GetCargoByIdAsync(userId, visao, cargo.Id);

                    return _mapper.Map<CargoDto>(cargoRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteCargo(int userId, string visao, int cargoId)
        {
            try
            {
                var cargo = await _cargoPersistence.GetCargoByIdAsync(userId, visao, cargoId);

                if (cargo == null) throw new Exception("Cargo não encontrado para exclusão");


                _globalPersistence.Delete<Cargo>(cargo);

                return await _globalPersistence.SaveChangeAsync();
            }

            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<CargoDto[]> GetAllCargosAsync( int userId, string visao)
        {
            try
            {
                var cargos = await _cargoPersistence.GetAllCargosAsync( userId,  visao);

                if (cargos == null) return null;

                var cargosMapper = _mapper.Map<CargoDto[]>(cargos);

                return cargosMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<CargoDto[]> GetAllCargosByNomeCargoAsync(int userId, string visao, string nome)
        {
            try
            {
                var cargos = await _cargoPersistence.GetAllCargosByNomeCargoAsync( userId,  visao, nome);

                if (cargos == null) return null;

                var cargosMapper = _mapper.Map<CargoDto[]>(cargos);

                return cargosMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<CargoDto> GetCargoByIdAsync(int userId, string visao, int cargoId)
        {
            try
            {
                var cargo = await _cargoPersistence.GetCargoByIdAsync( userId,  visao, cargoId);

                if (cargo == null) return null;

                var cargoMapper = _mapper.Map<CargoDto>(cargo);

                return cargoMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<CargoDto> UpdateCargo(int userId, string visao, int cargoId, CargoDto model)
        {
            try
            {
                var cargo = await _cargoPersistence.GetCargoByIdAsync( userId,  visao, cargoId);

                if (cargo == null) return null;

                model.Id = cargo.Id;

                _mapper.Map(model, cargo);

                _globalPersistence.Update<Cargo>(cargo);

                if (await _globalPersistence.SaveChangeAsync())
                {
                    var cargoRetorno = await _cargoPersistence.GetCargoByIdAsync( userId,  visao, cargo.Id);

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