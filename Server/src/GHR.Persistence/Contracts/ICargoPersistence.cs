using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHR.Domain;

namespace GHR.Persistence.Contracts
{
    public interface ICargoPersistence : IGlobalPersistence
    {
        Task<Cargo[]> GetAllCargosByNomeCargoAsync(int userId, string visao, string nome);
        Task<Cargo[]> GetAllCargosAsync(int userId, string visao);
        Task<Cargo> GetCargoByIdAsync(int userId, string visao, int cargoId);
    }
}