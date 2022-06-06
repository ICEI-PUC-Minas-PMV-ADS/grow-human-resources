using System.Linq;
using System.Threading.Tasks;
using GHR.Domain.DataBase.Cargos;
using GHR.Persistence.Interfaces.Contracts.Cargos;
using GHR.Persistence.Interfaces.Contexts;
using GHR.Persistence.Interfaces.Implements.Global;

using Microsoft.EntityFrameworkCore;
using GHR.Persistence.Models;

namespace GHR.Persistence.Interfaces.Implements.Cargos
{
    public class CargoPersistence : GlobalPersistence, ICargoPersistence
    {

        private readonly GHRContext _context;

        public CargoPersistence(GHRContext context) : base(context)
        {
            _context = context;
        }
        public async Task<PaginaLista<Cargo>> RecuperarCargosAsync(PaginaParametros paginaParametros, int empresaId)
        {
            IQueryable<Cargo> query = _context.Cargos
                .Include(d => d.Departamentos)
                .Include(e => e.Empresas);

            query = query
                .AsNoTracking()
                .OrderBy(c => c.Id)
                .Where(c => c.Id > 1 && c.EmpresaId == empresaId &&
                    (c.NomeCargo.ToLower().Contains(paginaParametros.Termo.ToLower()) ||
                     c.Funcao.ToLower().Contains(paginaParametros.Termo.ToLower()) ||
                     c.Departamentos.NomeDepartamento.ToLower().Contains(paginaParametros.Termo.ToLower())));

            return await PaginaLista<Cargo>.CriarPaginaAsync(query, paginaParametros.NumeroDaPagina, paginaParametros.TamanhoDaPagina); 
        }

        public async Task<Cargo> RecuperarCargoPorIdAsync(int cargoId, int empresaId)
        {
            IQueryable<Cargo> query = _context.Cargos
                .Include(d => d.Departamentos)
                .Include(e => e.Empresas);

            query = query
                .AsNoTracking()
                .OrderBy(c => c.Id)
                .Where(c => c.Id == cargoId && c.EmpresaId == empresaId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Cargo[]> RecuperarCargosPorDepartamentoIdAsync(int departamentoId, int empresaId)
        {
            IQueryable<Cargo> query = _context.Cargos
                .Include(d => d.Departamentos)
                .Include(e => e.Empresas);

            query = query
                .AsNoTracking()
                .OrderBy(c => c.Id)
                .Where(c => c.DepartamentoId == departamentoId && c.EmpresaId == empresaId);

            return await query.ToArrayAsync();
        }
    }  
}