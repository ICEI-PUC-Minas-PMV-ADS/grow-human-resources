using System;
using System.Threading.Tasks;
using AutoMapper;
using GHR.Application.Dtos.Funcionarios;
using GHR.Application.Services.Contracts.Funcionarios;
using GHR.Domain.DataBase.Funcionarios;
using GHR.Persistence.Interfaces.Contracts.Funcionarios;
using GHR.Persistence.Interfaces.Contracts.Global;

namespace GHR.Application.Services.Implements.Funcionarios
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
        public async Task<FuncionarioMetaDto> CriarFuncionarioMeta(int userId, string visao, FuncionarioMetaDto model)
        {
            try
            {
                var funcionarioMeta = _mapper.Map<FuncionarioMeta>(model);

                _globalPersistence.Cadastrar<FuncionarioMeta>(funcionarioMeta);
                if (await _globalPersistence.SalvarAsync())
                {
                    var funcionarioMetaRetorno = await _funcionarioMetaPersistence
                        .RecuperarFuncionarioMetaAsync(userId, visao, funcionarioMeta.FuncionarioId, funcionarioMeta.MetaId);

                    return _mapper.Map<FuncionarioMetaDto>(funcionarioMetaRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<FuncionarioMetaDto> AlterarFuncionarioMeta(int userId, string visao, int funcionarioId, int metaId, FuncionarioMetaDto model)
        {
            try
            {
                var funcionarioMeta = await _funcionarioMetaPersistence
                    .RecuperarFuncionarioMetaAsync( userId,  visao, funcionarioId, metaId);

                if (funcionarioMeta == null) return null;

                model.FuncionarioId = funcionarioMeta.FuncionarioId;
                model.MetaId = funcionarioMeta.MetaId;

                _mapper.Map(model, funcionarioMeta);

                _globalPersistence.Alterar<FuncionarioMeta>(funcionarioMeta);

                if (await _globalPersistence.SalvarAsync())
                {
                    var funcionarioMetaRetorno = await _funcionarioMetaPersistence
                        .RecuperarFuncionarioMetaAsync( userId,  visao, funcionarioMeta.FuncionarioId, funcionarioMeta.MetaId);

                    return _mapper.Map<FuncionarioMetaDto>(funcionarioMetaRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> ExcluirFuncionarioMeta(int userId, string visao, int funcionarioId, int metaId)
        {
            try
            {
                var funcionarioMeta = await _funcionarioMetaPersistence
                    .RecuperarFuncionarioMetaAsync( userId,  visao, funcionarioId, metaId);

                if (funcionarioMeta == null) throw new Exception("Funcionário/Meta não encontrado para exclusão");
                

                _globalPersistence.Excluir<FuncionarioMeta>(funcionarioMeta);

                return await _globalPersistence.SalvarAsync();
            }

            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

           } 

        public async Task<FuncionarioMetaDto[]> RecuperarMetasPorFuncionarioIdAsync(int userId, string visao, int funcionarioId)
        {
            try
            {
                var funcionarioMeta = await _funcionarioMetaPersistence
                    .RecuperarMetasPorFuncionarioIdAsync(userId, visao, funcionarioId);

                if (funcionarioMeta == null) return null;

                var funcionarioMetaMapper = _mapper.Map<FuncionarioMetaDto[]>(funcionarioMeta);

                return funcionarioMetaMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<FuncionarioMetaDto> RecuperarFuncionarioMetaPorIdAsync(int userId,  string visao, int funcionarioId, int metaId)
        {
            try
            {
                var funcionarioMeta = await _funcionarioMetaPersistence
                    .RecuperarFuncionarioMetaAsync( userId,  visao, funcionarioId, metaId);

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