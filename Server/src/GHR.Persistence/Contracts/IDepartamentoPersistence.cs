using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHR.Domain;

namespace GHR.Persistence.Contracts
{
    public interface IDepartamentoPersistence
    {
        //Funcionarios
        Task<Departamento[]> GetAllDepartamentosByNomeDepartamentoAsync(string nome);
        Task<Departamento[]> GetAllDepartamentosAsync();
        Task<Departamento> GetDepartamentoByIdAsync(int departamentoId);
    }
}