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
    public class MetaService : IMetaService
    {
        private readonly IGlobalPersistence _globalPersistence;
        private readonly IMetaPersistence _metaPersistence;
        private readonly IMapper _mapper;

        public MetaService(
            IGlobalPersistence globalPersistence,
            IMetaPersistence metaPersistence,
            IMapper mapper)
        {
            _globalPersistence = globalPersistence;
            _metaPersistence = metaPersistence;
            _mapper = mapper;
        }
        public async Task<MetaDto> AddMeta(int userId, string visao, MetaDto model)
        {
            try
            {
                var meta = _mapper.Map<Meta>(model);

                _globalPersistence.Add<Meta>(meta);
                if (await _globalPersistence.SaveChangeAsync())
                {
                    var metaRetorno = await _metaPersistence.GetMetaByIdAsync( userId,  visao, meta.Id, false);

                    return _mapper.Map<MetaDto>(metaRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<MetaDto> UpdateMeta(int userId, string visao, int metaId, MetaDto model)
        {
            try
            {
                var meta = await _metaPersistence.GetMetaByIdAsync( userId,  visao, metaId, false);

                if (meta == null) return null;

                model.Id = meta.Id;

                _mapper.Map(model, meta);

                _globalPersistence.Update<Meta>(meta);

                if (await _globalPersistence.SaveChangeAsync())
                {
                    var metaRetorno = await _metaPersistence.GetMetaByIdAsync( userId,  visao, meta.Id, false);

                    return _mapper.Map<MetaDto>(metaRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> DeleteMeta(int userId, string visao, int metaId)
        {
            try
            {
                var meta = await _metaPersistence.GetMetaByIdAsync( userId,  visao, metaId, false);

                if (meta == null) throw new Exception("Funcionário não encontrado para exclusão");


                _globalPersistence.Delete<Meta>(meta);

                return await _globalPersistence.SaveChangeAsync();
            }

            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        public async Task<MetaDto[]> GetAllMetasAsync(int userId, string visao, bool incluirFuncionario = false)
        {
            try
            {
                var metas = await _metaPersistence.GetAllMetasAsync( userId,  visao, incluirFuncionario);

                if (metas == null) return null;

                var metasMapper = _mapper.Map<MetaDto[]>(metas);

                return metasMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<MetaDto[]> GetAllMetasByNomeMetaAsync(int userId, string visao, string nome, bool incluirFuncionarios = false)
        {
            try
            {
                var metas = await _metaPersistence.GetAllMetasByNomeMetaAsync( userId,  visao, nome, incluirFuncionarios);

                if (metas == null) return null;

                var metasMapper = _mapper.Map<MetaDto[]>(metas);

                return metasMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<MetaDto[]> GetAllMetasByDescricaoMetaAsync(int userId, string visao, string descricao, bool incluirFuncionarios = false)
        {
            try
            {
                var metas = await _metaPersistence.GetAllMetasByDescricaoMetaAsync( userId,  visao, descricao, incluirFuncionarios);

                if (metas == null) return null;

                var metasMapper = _mapper.Map<MetaDto[]>(metas);

                return metasMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<MetaDto[]> GetAllMetasByMetaAprovadaAsync(int userId, string visao, bool metaAprovada, bool incluirFuncionarios = false)
        {
            try
            {
                var metas = await _metaPersistence.GetAllMetasByMetaAprovadaAsync( userId,  visao, metaAprovada, incluirFuncionarios);

                if (metas == null) return null;

                var metasMapper = _mapper.Map<MetaDto[]>(metas);

                return metasMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<MetaDto[]> GetAllMetasByMetaCumpridaAsync(int userId, string visao, bool metaCumprida, bool incluirFuncionarios = false)
        {
            try
            {
                var metas = await _metaPersistence.GetAllMetasByMetaCumpridaAsync( userId,  visao, metaCumprida, incluirFuncionarios);

                if (metas == null) return null;

                var metasMapper = _mapper.Map<MetaDto[]>(metas);

                return metasMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<MetaDto> GetMetaByIdAsync(int userId, string visao, int metaId, bool incluirFuncionarios = false)
        {
            try
            {
                var meta = await _metaPersistence.GetMetaByIdAsync( userId,  visao, metaId, incluirFuncionarios);

                if (meta == null) return null;

                var metaMapper = _mapper.Map<MetaDto>(meta);

                return metaMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

    }
}