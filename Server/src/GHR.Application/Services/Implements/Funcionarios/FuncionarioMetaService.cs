using System;
using System.Threading.Tasks;
using AutoMapper;
using GHR.Application.Dtos.Funcionarios;
using GHR.Application.Services.Contracts.Funcionarios;
using GHR.Domain.DataBase.Funcionarios;
using GHR.Persistence.Interfaces.Contracts.Funcionarios;
using GHR.Persistence.Interfaces.Contracts.Global;
using GHR.Persistence.Models;

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
        public async Task<FuncionarioMetaDto> CriarFuncionarioMeta( FuncionarioMetaDto model)
        {
            try
            {
                var funcionarioMeta = _mapper.Map<FuncionarioMeta>(model);

                _globalPersistence
                    .Cadastrar<FuncionarioMeta>(funcionarioMeta);
                if (await _globalPersistence.SalvarAsync())
                {
                    var funcionarioMetaRetorno = await _funcionarioMetaPersistence
                        .RecuperarFuncionarioMetaAsync(funcionarioMeta.FuncionarioId, funcionarioMeta.MetaId);

                    return _mapper.Map<FuncionarioMetaDto>(funcionarioMetaRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<FuncionarioMetaDto> AlterarFuncionarioMeta(FuncionarioMetaDto model)
        {
            try
            {
                var funcionarioMeta = await _funcionarioMetaPersistence
                    .RecuperarFuncionarioMetaAsync(model.FuncionarioId, model.MetaId);

                if (funcionarioMeta == null) return null;

                model.MetaId = funcionarioMeta.MetaId;
                model.FuncionarioId = funcionarioMeta.FuncionarioId;

                _mapper.Map(model, funcionarioMeta);

                _globalPersistence.Alterar<FuncionarioMeta>(funcionarioMeta);

                if (await _globalPersistence.SalvarAsync())
                {
                    var funcionarioMetaRetorno = await _funcionarioMetaPersistence
                        .RecuperarFuncionarioMetaAsync( funcionarioMeta.FuncionarioId, funcionarioMeta.MetaId);

                    return _mapper.Map<FuncionarioMetaDto>(funcionarioMetaRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> ExcluirFuncionarioMeta(int funcionarioId, int metaId)
        {
            try
            {
                var funcionarioMeta = await _funcionarioMetaPersistence
                    .RecuperarFuncionarioMetaAsync(funcionarioId, metaId);

                if (funcionarioMeta == null) throw new Exception("Funcionário/Meta não encontrado para exclusão");
                

                _globalPersistence.Excluir<FuncionarioMeta>(funcionarioMeta);

                return await _globalPersistence.SalvarAsync();
            }

            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

           } 

        public async Task<PaginaLista<FuncionarioMetaDto>> RecuperarMetasPorFuncionarioIdAsync(int funcionarioId, PaginaParametros paginaParametros)
        {
            try
            {
                var funcionarioMeta = await _funcionarioMetaPersistence
                    .RecuperarMetasPorFuncionarioIdAsync(funcionarioId, paginaParametros);

                if (funcionarioMeta == null) return null;

                var funcionarioMetaMapper = _mapper.Map<PaginaLista<FuncionarioMetaDto>>(funcionarioMeta);

                funcionarioMetaMapper.PaginaAtual = funcionarioMeta.PaginaAtual;
                funcionarioMetaMapper.TotalDePaginas = funcionarioMeta.TotalDePaginas;
                funcionarioMetaMapper.TamanhoDaPagina = funcionarioMeta.TamanhoDaPagina;
                funcionarioMetaMapper.ContadorTotal = funcionarioMeta.ContadorTotal;

                return funcionarioMetaMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<FuncionarioMetaDto> RecuperarFuncionarioMetaPorIdAsync(int funcionarioId, int metaId)
        {
            try
            {
                var funcionarioMeta = await _funcionarioMetaPersistence
                    .RecuperarFuncionarioMetaAsync(funcionarioId, metaId);

                if (funcionarioMeta == null) return null;

                var funcionarioMetaMapper = _mapper.Map<FuncionarioMetaDto>(funcionarioMeta);

                return funcionarioMetaMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<FuncionarioMetaDto[]> RecuperarFuncionariosMetasAsync()
        {
            try
            {
                var funcionarioMeta = await _funcionarioMetaPersistence
                    .RecuperarFuncionariosMetasAsync();

                if (funcionarioMeta == null) return null;

                var funcionarioMetaMapper = _mapper.Map<FuncionarioMetaDto[]>(funcionarioMeta);

                return funcionarioMetaMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}