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
    public class SupervisorPersistence : ISupervisorPersistence
    {

        private readonly GHRContext _context;

        public SupervisorPersistence(GHRContext context)
        {
            _context = context;
        }

        public async Task<Supervisor> GetSupervisorByIdAsync(int supervisorId)
        {
            IQueryable<Supervisor> query = _context.Supervisores;

            query = query
                .AsNoTracking()
                .OrderBy(s => s.Id)
                .Where(s => s.Id == supervisorId);

            return await query.FirstOrDefaultAsync();
        }
        public async Task<Supervisor[]> GetAllSupervisoresAsync()
        {
            IQueryable<Supervisor> query = _context.Supervisores;

            query = query
                .AsNoTracking()
                .OrderBy(s => s.Id);

            return await query.ToArrayAsync();
        }
    }
}