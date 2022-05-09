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
    public class DepartamentoPersistence : IDepartamentoPersistence
    {

        private readonly GHRContext _context;

        public DepartamentoPersistence(GHRContext context)
        {
            _context = context;
        }
        public async Task<Departamento[]> GetAllDepartamentosAsync()
        {
            IQueryable<Departamento> query = _context.Departamentos;

            query = query
                .AsNoTracking()
                .OrderBy(f => f.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Departamento[]> GetAllDepartamentosByNomeDepartamentoAsync(string nome)
        {
            IQueryable<Departamento> query = _context.Departamentos;

            query = query
                .AsNoTracking()
                .OrderBy(d => d.Id)
                .Where(d => d.NomeDepartamento.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Departamento> GetDepartamentoByIdAsync(int departamentoId)
        {
            IQueryable<Departamento> query = _context.Departamentos;

            query = query
                .AsNoTracking()
                .OrderBy(d => d.Id)
                .Where(d => d.Id == departamentoId);

            return await query.FirstOrDefaultAsync();
        }
    }
}