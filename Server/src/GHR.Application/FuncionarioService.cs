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
    public class FuncionarioService : IFuncionarioService
    {
        private readonly IGlobalPersistence _globalPersistence;
        private readonly IFuncionarioPersistence _funcionarioPersistence;
        private readonly IMapper _mapper;

        public FuncionarioService(
            IGlobalPersistence globalPersistence, 
            IFuncionarioPersistence funcionarioPersistence,
            IMapper mapper)
        {
            _globalPersistence = globalPersistence;
            _funcionarioPersistence = funcionarioPersistence;
            _mapper = mapper;
        }
        public async Task<FuncionarioDto> AddFuncionarios(int userId, string visao, FuncionarioDto model)
        {
            try
            {
                var funcionario = _mapper.Map<Funcionario>(model);

                _globalPersistence.Add<Funcionario>(funcionario);
                if (await _globalPersistence.SaveChangeAsync())
                {
                    var funcionarioRetorno = await _funcionarioPersistence.GetFuncionarioByIdAsync( userId,  visao, funcionario.Id, false);

                    return _mapper.Map<FuncionarioDto>(funcionarioRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<FuncionarioDto> UpdateFuncionario(int userId, string visao, int funcionarioId, FuncionarioDto model)
        {
            try
            {
                var funcionario = await _funcionarioPersistence.GetFuncionarioByIdAsync( userId,  visao, funcionarioId, false);

                if (funcionario == null) return null;

                model.Id = funcionario.Id;

                _mapper.Map(model, funcionario);

                _globalPersistence.Update<Funcionario>(funcionario);

                if (await _globalPersistence.SaveChangeAsync())
                {
                    var funcionarioRetorno = await _funcionarioPersistence.GetFuncionarioByIdAsync( userId,  visao, funcionario.Id, false);

                    return _mapper.Map<FuncionarioDto>(funcionarioRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> DeleteFuncionario(int userId, string visao, int funcionarioId)
        {
            try
            {
                var funcionario = await _funcionarioPersistence.GetFuncionarioByIdAsync( userId,  visao, funcionarioId, false);

                if (funcionario == null) throw new Exception("Funcionário não encontrado para exclusão");
                

                _globalPersistence.Delete<Funcionario>(funcionario);

                return await _globalPersistence.SaveChangeAsync();
            }

            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

           } 

        public async Task<FuncionarioDto[]> GetAllFuncionariosAsync(int userId, string visao, bool incluirMetas = false)
        {
            try
            {
                var funcionarios = await _funcionarioPersistence.GetAllFuncionariosAsync( userId,  visao, incluirMetas);

                if (funcionarios == null) return null;

                var funcionariosMapper = _mapper.Map<FuncionarioDto[]>(funcionarios);

                return funcionariosMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<FuncionarioDto[]> GetAllFuncionariosByNomeCompletoAsync(int userId, string visao, string nome, bool incluirMetas = false)
        {
            try
            {
                var funcionarios = await _funcionarioPersistence.GetAllFuncionariosByNomeCompletoAsync( userId,  visao, nome, incluirMetas);

                if (funcionarios == null) return null;

                var funcionariosMapper = _mapper.Map<FuncionarioDto[]>(funcionarios);

                return funcionariosMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<FuncionarioDto> GetFuncionarioByIdAsync(int userId, string visao, int funcionarioId, bool incluirMetas = false)
        {
            try
            {
                var funcionario = await _funcionarioPersistence.GetFuncionarioByIdAsync( userId,  visao, funcionarioId, incluirMetas);

                if (funcionario == null) return null;

                var funcionarioMapper = _mapper.Map<FuncionarioDto>(funcionario);

                return funcionarioMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

    }
}