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
    public class CargoPersistence : ICargoPersistence
    {

        private readonly GHRContext _context;

        public CargoPersistence(GHRContext context)
        {
            _context = context;
        }
        public async Task<Cargo[]> GetAllCargosAsync()
        {
            IQueryable<Cargo> query = _context.Cargos;

            query = query
                .AsNoTracking()
                .OrderBy(c => c.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Cargo[]> GetAllCargosByNomeCargoAsync(string nome)
        {
            IQueryable<Cargo> query = _context.Cargos;

            query = query
                .AsNoTracking()
                .OrderBy(c => c.Id)
                .Where(c => c.NomeCargo.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Cargo> GetCargoByIdAsync(int cargoId)
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