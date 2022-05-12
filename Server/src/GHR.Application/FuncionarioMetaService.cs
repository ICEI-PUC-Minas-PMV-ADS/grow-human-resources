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
    public class FuncionarioMetaService : IFuncionarioMetaService
    {
        private readonly IGlobalPersistence _globalPersistence;
        private readonly IFuncionarioMetaPersistence _funcionarioMetaPersistence;
        private readonly IMapper _mapper;

        public FuncionarioMetaService(
            IGlobalPersistence globalPersistence, 
            IFuncionarioMetaPersistence funcionarioMetaPersistence,
            IMapper mapper)
        {
            _globalPersistence = globalPersistence;
            _funcionarioMetaPersistence = funcionarioMetaPersistence;
            _mapper = mapper;
        }
        public async Task<FuncionarioMetaDto> AddFuncionarioMeta(FuncionarioMetaDto model)
        {
            try
            {
                var funcionarioMeta = _mapper.Map<FuncionarioMeta>(model);

                _globalPersistence.Add<FuncionarioMeta>(funcionarioMeta);
                if (await _globalPersistence.SaveChangeAsync())
                {
                    var funcionarioMetaRetorno = await _funcionarioMetaPersistence.GetFuncionarioMetaAsync(funcionarioMeta.FuncionarioId, funcionarioMeta.MetaId);

                    return _mapper.Map<FuncionarioMetaDto>(funcionarioMetaRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<FuncionarioMetaDto> UpdateFuncionarioMeta(int funcionarioId, int metaId, FuncionarioMetaDto model)
        {
            try
            {
                var funcionarioMeta = await _funcionarioMetaPersistence.GetFuncionarioMetaAsync(funcionarioId, metaId);

                if (funcionarioMeta == null) return null;

                model.FuncionarioId = funcionarioMeta.FuncionarioId;
                model.MetaId = funcionarioMeta.MetaId;

                _mapper.Map(model, funcionarioMeta);

                _globalPersistence.Update<FuncionarioMeta>(funcionarioMeta);

                if (await _globalPersistence.SaveChangeAsync())
                {
                    var funcionarioMetaRetorno = await _funcionarioMetaPersistence.GetFuncionarioMetaAsync(funcionarioMeta.FuncionarioId, funcionarioMeta.MetaId);

                    return _mapper.Map<FuncionarioMetaDto>(funcionarioMetaRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> DeleteFuncionarioMeta(int funcionarioId, int metaId)
        {
            try
            {
                var funcionarioMeta = await _funcionarioMetaPersistence.GetFuncionarioMetaAsync(funcionarioId, metaId);

                if (funcionarioMeta == null) throw new Exception("Funcionário/Meta não encontrado para exclusão");
                

                _globalPersistence.Delete<FuncionarioMeta>(funcionarioMeta);

                return await _globalPersistence.SaveChangeAsync();
            }

            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

           } 

        public async Task<FuncionarioMetaDto[]> GetMetasByFuncionarioIdAsync(int funcionarioId)
        {
            try
            {
                var funcionarioMeta = await _funcionarioMetaPersistence.GetMetasByFuncionarioIdAsync(funcionarioId);

                if (funcionarioMeta == null) return null;

                var funcionarioMetaMapper = _mapper.Map<FuncionarioMetaDto[]>(funcionarioMeta);

                return funcionarioMetaMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<FuncionarioMetaDto> GetFuncionarioMetaAsync(int funcionarioId, int metaId)
        {
            try
            {
                var funcionarioMeta = await _funcionarioMetaPersistence.GetFuncionarioMetaAsync(funcionarioId, metaId);

                if (funcionarioMeta == null) return null;

                var funcionarioMetaMapper = _mapper.Map<FuncionarioMetaDto>(funcionarioMeta);

                return funcionarioMetaMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

    }
}