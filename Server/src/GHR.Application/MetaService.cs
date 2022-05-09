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
        public async Task<MetaDto> AddMeta(MetaDto model)
        {
            try
            {
                var meta = _mapper.Map<Meta>(model);

                _globalPersistence.Add<Meta>(meta);
                if (await _globalPersistence.SaveChangeAsync())
                {
                    var metaRetorno = await _metaPersistence.GetMetaByIdAsync(meta.Id, false);

                    return _mapper.Map<MetaDto>(metaRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<MetaDto> UpdateMeta(int metaId, MetaDto model)
        {
            try
            {
                var meta = await _metaPersistence.GetMetaByIdAsync(metaId, false);

                if (meta == null) return null;

                model.Id = meta.Id;

                _mapper.Map(model, meta);

                _globalPersistence.Update<Meta>(meta);

                if (await _globalPersistence.SaveChangeAsync())
                {
                    var metaRetorno = await _metaPersistence.GetMetaByIdAsync(meta.Id, false);

                    return _mapper.Map<MetaDto>(metaRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> DeleteMeta(int metaId)
        {
            try
            {
                var meta = await _metaPersistence.GetMetaByIdAsync(metaId, false);

                if (meta == null) throw new Exception("Funcionário não encontrado para exclusão");


                _globalPersistence.Delete<Meta>(meta);

                return await _globalPersistence.SaveChangeAsync();
            }

            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        public async Task<MetaDto[]> GetAllMetasAsync(bool incluirFuncionario = false)
        {
            try
            {
                var metas = await _metaPersistence.GetAllMetasAsync(incluirFuncionario);

                if (metas == null) return null;

                var metasMapper = _mapper.Map<MetaDto[]>(metas);

                return metasMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<MetaDto[]> GetAllMetasByNomeMetaAsync(string nome, bool incluirFuncionarios = false)
        {
            try
            {
                var metas = await _metaPersistence.GetAllMetasByNomeMetaAsync(nome, incluirFuncionarios);

                if (metas == null) return null;

                var metasMapper = _mapper.Map<MetaDto[]>(metas);

                return metasMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<MetaDto[]> GetAllMetasByDescricaoMetaAsync(string descricao, bool incluirFuncionarios = false)
        {
            try
            {
                var metas = await _metaPersistence.GetAllMetasByDescricaoMetaAsync(descricao, incluirFuncionarios);

                if (metas == null) return null;

                var metasMapper = _mapper.Map<MetaDto[]>(metas);

                return metasMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<MetaDto[]> GetAllMetasByMetaAprovadaAsync(bool metaAprovada, bool incluirFuncionarios = false)
        {
            try
            {
                var metas = await _metaPersistence.GetAllMetasByMetaAprovadaAsync(metaAprovada, incluirFuncionarios);

                if (metas == null) return null;

                var metasMapper = _mapper.Map<MetaDto[]>(metas);

                return metasMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<MetaDto[]> GetAllMetasByMetaCumpridaAsync(bool metaCumprida, bool incluirFuncionarios = false)
        {
            try
            {
                var metas = await _metaPersistence.GetAllMetasByMetaCumpridaAsync(metaCumprida, incluirFuncionarios);

                if (metas == null) return null;

                var metasMapper = _mapper.Map<MetaDto[]>(metas);

                return metasMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<MetaDto> GetMetaByIdAsync(int metaId, bool incluirFuncionarios = false)
        {
            try
            {
                var meta = await _metaPersistence.GetMetaByIdAsync(metaId, incluirFuncionarios);

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