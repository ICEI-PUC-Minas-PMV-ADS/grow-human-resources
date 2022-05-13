using System.Linq;
using System.Threading.Tasks;
using GHR.Domain;
using GHR.Persistence.Contexts;
using GHR.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GHR.Persistence.Context
{
    public class FuncionarioPersistence : IFuncionarioPersistence
    {
        private readonly GHRContext _context;

        public FuncionarioPersistence(GHRContext context)
        {
            _context = context;
        }
        //Funcionarios
        public async Task<Funcionario[]> GetAllFuncionariosAsync(bool incluirMetas = false)
        {
            IQueryable<Funcionario> query = _context.Funcionarios
                .Include(f => f.Cargo)
                .Include(f => f.Departamento)
                .Include(f => f.Supervisor)
                .Include(f => f.Login);

            if (incluirMetas)
            {
                query = query
                    .Include(fm => fm.FuncionariosMetas)
                    .ThenInclude(m => m.Meta);
            }

            query = query
                .AsNoTracking()
                .OrderBy(f => f.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Funcionario[]> GetAllFuncionariosByNomeCompletoAsync(string nome, bool incluirMetas = false)
        {
            IQueryable<Funcionario> query = _context.Funcionarios
                .Include(c => c.Cargo)
                .Include(d => d.Departamento)
                .Include(s => s.Supervisor)
                .Include(l => l.Login);

            if (incluirMetas)
            {
                query = query
                    .Include(fm => fm.FuncionariosMetas)
                    .ThenInclude(m => m.Meta);
            }

            query = query
                .AsNoTracking()
                .OrderBy(f => f.Id)
                .Where(f => f.NomeCompleto.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }
        public async Task<Funcionario> GetFuncionarioByIdAsync(int funcionarioId, bool incluirMetas = false)
        {
            IQueryable<Funcionario> query = _context.Funcionarios
                .Include(f => f.Cargo)
                .Include(f => f.Departamento)
                .Include(f => f.Supervisor)
                .Include(f => f.Login);

            if (incluirMetas)
            {
                query = query
                    .Include(fm => fm.FuncionariosMetas)
                    .ThenInclude(m => m.Meta);
            }

            query = query
                .AsNoTracking()
                .OrderBy(f => f.Id)
                .Where(f => f.Id == funcionarioId);

            return await query.FirstOrDefaultAsync();
        }
    }
}