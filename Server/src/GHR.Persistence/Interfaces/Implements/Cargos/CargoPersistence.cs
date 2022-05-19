using System.Linq;
using System.Threading.Tasks;
using GHR.Domain.DataBase.Cargos;
using GHR.Persistence.Interfaces.Contracts.Cargos;
using GHR.Persistence.Interfaces.Contexts;
using GHR.Persistence.Interfaces.Implements.Global;

using Microsoft.EntityFrameworkCore;

namespace GHR.Persistence.Interfaces.Implements.Cargos
{
    public class CargoPersistence : GlobalPersistence, ICargoPersistence
    {

        private readonly GHRContext _context;

        public CargoPersistence(GHRContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Cargo[]> RecuperarCargosAsync()
        {
            IQueryable<Cargo> query = _context.Cargos;

            query = query
                .AsNoTracking()
                .OrderBy(c => c.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Cargo[]> RecuperarCargosPorNomeCargoAsync(string nome)
        {
            IQueryable<Cargo> query = _context.Cargos;

            query = query
                .AsNoTracking()
                .OrderBy(c => c.Id)
                .Where(c => c.NomeCargo.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Cargo> RecuperarCargoPorIdAsync(int cargoId)
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