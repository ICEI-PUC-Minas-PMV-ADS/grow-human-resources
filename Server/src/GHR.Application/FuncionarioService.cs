using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHR.Application.Contracts;
using GHR.Domain;
using GHR.Persistence.Contracts;

namespace GHR.Application
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly IGlobalPersistence _geralPersistence;
        private readonly IFuncionarioPersistence _funcionarioPersistence;

        public FuncionarioService(IGlobalPersistence geralPersistence, IFuncionarioPersistence funcionarioPersistence)
        {
            _funcionarioPersistence = funcionarioPersistence;
            _geralPersistence = geralPersistence;
        }
        public async Task<Funcionario> AddFuncionarios(Funcionario model)
        {
            try
            {
                _geralPersistence.Add<Funcionario>(model);
                if (await _geralPersistence.SaveChangeAsync())
                {
                    return await _funcionarioPersistence.GetFuncionarioByIdAsync(model.Id, false);
                }
                return null;
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<Funcionario> UpdateFuncionario(int funcionarioId, Funcionario model)
        {
            try
            {
                var funcionario = await _funcionarioPersistence.GetFuncionarioByIdAsync(funcionarioId, false);

                if (funcionario == null) return null;

                model.Id = funcionario.Id;

                _geralPersistence.Update(model);

                if (await _geralPersistence.SaveChangeAsync())
                {
                    return await _funcionarioPersistence.GetFuncionarioByIdAsync(model.Id, false);
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> DeleteFuncionario(int funcionarioId)
        {
            try
            {
                var funcionario = await _funcionarioPersistence.GetFuncionarioByIdAsync(funcionarioId, false);

                if (funcionario == null) throw new Exception("Funcionário não encontrado para exclusão");

                _geralPersistence.Delete<Funcionario>(funcionario);

                return await _geralPersistence.SaveChangeAsync();
            }

            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        public async Task<Funcionario[]> GetAllFuncionariosAsync(bool incluirMetas = false)
        {
            try
            {
                var funcionarios = await _funcionarioPersistence.GetAllFuncionariosAsync(incluirMetas);

                if (funcionarios == null) return null;

                return funcionarios;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Funcionario[]> GetAllFuncionariosByNomeCompletoAsync(string nome, bool incluirMetas = false)
        {
            try
            {
                var funcionarios = await _funcionarioPersistence.GetAllFuncionariosByNomeCompletoAsync(nome, incluirMetas);

                if (funcionarios == null) return null;

                return funcionarios;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Funcionario> GetFuncionarioByIdAsync(int funcionarioId, bool incluirMetas = false)
        {
            try
            {
                var funcionario = await _funcionarioPersistence.GetFuncionarioByIdAsync(funcionarioId, incluirMetas);

                if (funcionario == null) return null;

                return funcionario;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

    }
}