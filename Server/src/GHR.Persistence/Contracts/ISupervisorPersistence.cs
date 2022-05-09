using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHR.Domain;

namespace GHR.Persistence.Contracts
{
    public interface ISupervisorPersistence
    {
        //Funcionarios
        Task<Supervisor> GetSupervisorByIdAsync(int supervisorId);
        Task<Supervisor[]> GetAllSupervisoresAsync();
    }
}