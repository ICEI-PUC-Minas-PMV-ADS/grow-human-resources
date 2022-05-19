using System;
using System.Linq;
using System.Threading.Tasks;
using GHR.Domain.DataBase.Departamentos;
using GHR.Persistence.Interfaces.Contexts;
using GHR.Persistence.Interfaces.Contracts.Departamentos;
using GHR.Persistence.Interfaces.Implements.Global;
using Microsoft.EntityFrameworkCore;

namespace GHR.Persistence.Interfaces.Implements.Departamentos
{
    public class DepartamentoPersistence : GlobalPersistence, IDepartamentoPersistence
    {

        private readonly GHRContext _context;

        public DepartamentoPersistence(GHRContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Departamento[]> RecuperarDepartamentosAsync()
        {
            IQueryable<Departamento> query = _context.Departamentos;

            query = query
                .AsNoTracking()
                .OrderBy(f => f.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Departamento[]> RecuperarDepartamentosPorNomeDepartamentoAsync(string nome)
        {
            IQueryable<Departamento> query = _context.Departamentos;

            query = query
                .AsNoTracking()
                .OrderBy(d => d.Id)
                .Where(d => d.NomeDepartamento.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Departamento> RecuperarDepartamentoPorIdAsync(int departamentoId)
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