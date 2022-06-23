using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHR.Domain.DataBase.Empresas;
using GHR.Persistence.Interfaces.Contexts;
using GHR.Persistence.Interfaces.Contracts.Empresas;
using GHR.Persistence.Interfaces.Implements.Global;
using Microsoft.EntityFrameworkCore;

namespace GHR.Persistence.Interfaces.Implements.Empresas
{
    public class EmpresaPersistence : GlobalPersistence, IEmpresaPersistence
    {
        private readonly GHRContext _context;

        public EmpresaPersistence(GHRContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Empresa> RecuperarEmpresaPorIdAsync(int id)
        {
            IQueryable<Empresa> query = _context.Empresas;

            query = query
                .Where(e => e.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Empresa>> RecuperarEmpresasAsync()
        {
            IQueryable<Empresa> query = _context.Empresas;

            query = query
                .AsNoTracking()
                .OrderBy(d => d.Id);

            return await query.ToListAsync();
        }
    }
}