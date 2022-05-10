using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHR.Domain;

namespace GHR.Persistence.Contracts
{
    public interface ICargoPersistence
    {
        Task<Cargo[]> GetAllCargosByNomeCargoAsync(string nome);
        Task<Cargo[]> GetAllCargosAsync();
        Task<Cargo> GetCargoByIdAsync(int cargoId);
    }
}