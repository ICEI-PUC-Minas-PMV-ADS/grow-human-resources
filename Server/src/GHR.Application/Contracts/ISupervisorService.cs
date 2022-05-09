using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHR.Application.Dtos;

namespace GHR.Application.Contracts
{
    public interface ISupervisorService
    {
        Task<SupervisorDto> AddSupervisor(SupervisorDto model);
        Task<SupervisorDto> UpdateSupervisor(int supervisorId, SupervisorDto model);
        Task<bool> DeleteSupervisor(int supervisorId);
        Task<SupervisorDto> GetSupervisorByIdAsync(int supervisorId);
        Task<SupervisorDto[]> GetAllSupervisoresAsync();
    }
}