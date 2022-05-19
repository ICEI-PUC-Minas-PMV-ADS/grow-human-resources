using System;
using System.Threading.Tasks;
using AutoMapper;
using GHR.Application.Dtos.Metas;
using GHR.Application.Serices.Contracts.Metas;
using GHR.Domain.DataBase.Metas;
using GHR.Persistence.Implements.Contracts.Metas;
using GHR.Persistence.Interfaces.Contracts.Global;

namespace GHR.Application.Services.Implements.Metas
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
        public async Task<MetaDto> CriarMeta(int userId, string visao, MetaDto model)
        {
            try
            {
                var meta = _mapper.Map<Meta>(model);

                _globalPersistence.Cadastrar<Meta>(meta);
                if (await _globalPersistence.SalvarAsync())
                {
                    var metaRetorno = await _metaPersistence
                        .RecuperarMetaPorIdAsync(meta.Id, false);

                    return _mapper.Map<MetaDto>(metaRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<MetaDto> AlterarMeta(int userId, string visao, int metaId, MetaDto model)
        {
            try
            {
                var meta = await _metaPersistence
                    .RecuperarMetaPorIdAsync(  metaId, false);

                if (meta == null) return null;

                model.Id = meta.Id;

                _mapper.Map(model, meta);

                _globalPersistence.Alterar<Meta>(meta);

                if (await _globalPersistence.SalvarAsync())
                {
                    var metaRetorno = await _metaPersistence
                        .RecuperarMetaPorIdAsync(meta.Id, false);

                    return _mapper.Map<MetaDto>(metaRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> ExcluirMeta(int userId, string visao, int metaId)
        {
            try
            {
                var meta = await _metaPersistence
                    .RecuperarMetaPorIdAsync(metaId, false);

                if (meta == null) throw new Exception("Funcionário não encontrado para exclusão");


                _globalPersistence.Excluir<Meta>(meta);

                return await _globalPersistence.SalvarAsync();
            }

            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        public async Task<MetaDto[]> RecuperarMetasAsync(int userId, string visao, bool incluirFuncionario = false)
        {
            try
            {
                var metas = await _metaPersistence
                    .RecuperarMetasAsync( userId,  visao, incluirFuncionario);

                if (metas == null) return null;

                var metasMapper = _mapper.Map<MetaDto[]>(metas);

                return metasMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<MetaDto[]> RecuperarMetasPorNomeMetaAsync(int userId, string visao, string nome, bool incluirFuncionarios = false)
        {
            try
            {
                var metas = await _metaPersistence
                    .RecuperarMetasPorNomeMetaAsync( userId,  visao, nome, incluirFuncionarios);

                if (metas == null) return null;

                var metasMapper = _mapper.Map<MetaDto[]>(metas);

                return metasMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<MetaDto[]> RecuperarMetasPorDescricaoMetaAsync(int userId, string visao, string descricao, bool incluirFuncionarios = false)
        {
            try
            {
                var metas = await _metaPersistence
                    .RecuperarMetasPorDescricaoMetaAsync( userId,  visao, descricao, incluirFuncionarios);

                if (metas == null) return null;

                var metasMapper = _mapper.Map<MetaDto[]>(metas);

                return metasMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<MetaDto[]> RecuperarMetasPorMetaAprovadaAsync(int userId, string visao, bool metaAprovada, bool incluirFuncionarios = false)
        {
            try
            {
                var metas = await _metaPersistence
                    .RecuperarMetasPorMetaAprovadaAsync( userId,  visao, metaAprovada, incluirFuncionarios);

                if (metas == null) return null;

                var metasMapper = _mapper.Map<MetaDto[]>(metas);

                return metasMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<MetaDto[]> RecuperarMetasPorMetaCumpridaAsync(int userId, string visao, bool metaCumprida, bool incluirFuncionarios = false)
        {
            try
            {
                var metas = await _metaPersistence
                    .RecuperarMetasPorMetaCumpridaAsync( userId,  visao, metaCumprida, incluirFuncionarios);

                if (metas == null) return null;

                var metasMapper = _mapper.Map<MetaDto[]>(metas);

                return metasMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<MetaDto> RecuperarMetaPorIdAsync(int userId, string visao, int metaId, bool incluirFuncionarios = false)
        {
            try
            {
                var meta = await _metaPersistence.RecuperarMetaPorIdAsync(  metaId, incluirFuncionarios);

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