using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GHR.Application.Contracts;
using GHR.Application.Dtos;
using GHR.Domain;
using GHR.Persistence.Contracts;

namespace GHR.Application
{
    public class SupervisorService : ISupervisorService
    {
        private readonly IGlobalPersistence _globalPersistence;
        private readonly ISupervisorPersistence _supervisorPersistence;
        private readonly IMapper _mapper;

        public SupervisorService(
            IGlobalPersistence globalPersistence,
            ISupervisorPersistence SupervisorPersistence,
            IMapper mapper)
        {
            _globalPersistence = globalPersistence;
            _supervisorPersistence = SupervisorPersistence;
            _mapper = mapper;
        }
        public async Task<SupervisorDto> AddSupervisor(SupervisorDto model)
        {
            try
            {
                var supervisor = _mapper.Map<Supervisor>(model);

                _globalPersistence.Add<Supervisor>(supervisor);
                if (await _globalPersistence.SaveChangeAsync())
                {
                    var supervisorRetorno = await _supervisorPersistence.GetSupervisorByIdAsync(supervisor.Id);

                    return _mapper.Map<SupervisorDto>(supervisorRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteSupervisor(int supervisorId)
        {
            try
            {
                var supervisor = await _supervisorPersistence.GetSupervisorByIdAsync(supervisorId);

                if (supervisor == null) throw new Exception("Supervisor não encontrado para exclusão");


                _globalPersistence.Delete<Supervisor>(supervisor);

                return await _globalPersistence.SaveChangeAsync();
            }

            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<SupervisorDto> GetSupervisorByIdAsync(int supervisorId)
        {
            try
            {
                var supervisor = await _supervisorPersistence.GetSupervisorByIdAsync(supervisorId);

                if (supervisor == null) return null;

                var supervisorMapper = _mapper.Map<SupervisorDto>(supervisor);

                return supervisorMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<SupervisorDto> UpdateSupervisor(int supervisorId, SupervisorDto model)
        {
            try
            {
                var supervisor = await _supervisorPersistence.GetSupervisorByIdAsync(supervisorId);

                if (supervisor == null) return null;

                model.Id = supervisor.Id;

                _mapper.Map(model, supervisor);

                _globalPersistence.Update<Supervisor>(supervisor);

                if (await _globalPersistence.SaveChangeAsync())
                {
                    var supervisorRetorno = await _supervisorPersistence.GetSupervisorByIdAsync(supervisor.Id);

                    return _mapper.Map<SupervisorDto>(supervisorRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<SupervisorDto[]> GetAllSupervisoresAsync()
        {
            try
            {
                var sueprvisores = await _supervisorPersistence.GetAllSupervisoresAsync();

                if (sueprvisores == null) return null;

                var supervisoresMapper = _mapper.Map<SupervisorDto[]>(sueprvisores);

                return supervisoresMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}