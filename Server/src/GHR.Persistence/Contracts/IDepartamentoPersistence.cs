using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHR.Domain;

namespace GHR.Persistence.Contracts
{
    public interface IDepartamentoPersistence : IGlobalPersistence 
    {
        //Funcionarios
        Task<Departamento[]> GetAllDepartamentosByNomeDepartamentoAsync(int userId, string visao, string nome);
        Task<Departamento[]> GetAllDepartamentosAsync(int userId, string visao);
        Task<Departamento> GetDepartamentoByIdAsync(int userId, string visao, int departamentoId);
    }
}