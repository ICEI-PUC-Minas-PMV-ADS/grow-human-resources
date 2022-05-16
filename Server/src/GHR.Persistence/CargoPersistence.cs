using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHR.Domain;
using GHR.Persistence.Contexts;
using GHR.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GHR.Persistence
{
    public class CargoPersistence : GlobalPersistence, ICargoPersistence
    {

        private readonly GHRContext _context;

        public CargoPersistence(GHRContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Cargo[]> GetAllCargosAsync(int userId, string visao)
        {
            IQueryable<Cargo> query = _context.Cargos;

            query = query
                .AsNoTracking()
                .OrderBy(c => c.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Cargo[]> GetAllCargosByNomeCargoAsync(int userId, string visao, string nome)
        {
            IQueryable<Cargo> query = _context.Cargos;

            query = query
                .AsNoTracking()
                .OrderBy(c => c.Id)
                .Where(c => c.NomeCargo.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Cargo> GetCargoByIdAsync(int userId, string visao, int cargoId)
        {
            IQueryable<Cargo> query = _context.Cargos;

            query = query
                .AsNoTracking()
                .OrderBy(c => c.Id)
                .Where(c => c.Id == cargoId);

            return await query.FirstOrDefaultAsync();
        }
    }  
}