using System.Linq;
using System.Threading.Tasks;
using GHR.Domain;
using GHR.Persistence.Contexts;
using GHR.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GHR.Persistence.Context
{
    public class FuncionarioPersistence : GlobalPersistence, IFuncionarioPersistence
    {
        private readonly GHRContext _context;

        public FuncionarioPersistence(GHRContext context) : base(context)
        {
            _context = context;
        }
        //Funcionarios
        public async Task<Funcionario[]> GetAllFuncionariosAsync(int userId, string visao, bool incluirMetas = false)
        {
            IQueryable<Funcionario> query = _context.Funcionarios
                .Include(c => c.Cargo)
                .Include(d => d.Departamento)
                .Include(u => u.User);

            if (incluirMetas)
            {
                query = query
                    .Include(fm => fm.FuncionariosMetas)
                    .ThenInclude(m => m.Meta);
            }

            if (visao.Contains("RH"))
                query = query.Where(f => f.UserId == userId);

            query = query
                .AsNoTracking()
                .OrderBy(f => f.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Funcionario[]> GetAllFuncionariosByNomeCompletoAsync(int userId, string visao, string nome, bool incluirMetas = false)
        {
            IQueryable<Funcionario> query = _context.Funcionarios
                .Include(c => c.Cargo)
                .Include(d => d.Departamento)
                .Include(u => u.User);

            if (incluirMetas)
            {
                query = query
                    .Include(fm => fm.FuncionariosMetas)
                    .ThenInclude(m => m.Meta);
            }

            query = !visao.Contains("RH") 
                  ? query.Where(f => f.UserId == userId && f.User.NomeCompleto.ToLower().Contains(nome.ToLower())) 
                  : query.Where(f => f.User.NomeCompleto.ToLower().Contains(nome.ToLower()));

            query = query
                .AsNoTracking()
                .OrderBy(f => f.Id);


            return await query.ToArrayAsync();
        }
        public async Task<Funcionario> GetFuncionarioByIdAsync(int userId, string visao, int funcionarioId, bool incluirMetas = false)
        {
            IQueryable<Funcionario> query = _context.Funcionarios
                .Include(c => c.Cargo)
                .Include(d => d.Departamento)
                .Include(u => u.User);

            if (incluirMetas)
            {
                query = query
                    .Include(fm => fm.FuncionariosMetas)
                    .ThenInclude(m => m.Meta);
            }

            query = !visao.Contains("RH")
                  ? query.Where(f => f.UserId == userId && f.Id == funcionarioId)
                  : query.Where(f => f.Id == funcionarioId);

            query = query
                .AsNoTracking()
                .OrderBy(f => f.Id)
                .Where(f => f.Id == funcionarioId);

            return await query.FirstOrDefaultAsync();
        }
    }
}