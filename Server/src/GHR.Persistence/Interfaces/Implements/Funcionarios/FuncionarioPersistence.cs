using System.Linq;
using System.Threading.Tasks;
using GHR.Domain.DataBase.Funcionarios;
using GHR.Persistence.Interfaces.Contexts;
using GHR.Persistence.Interfaces.Contracts.Funcionarios;
using GHR.Persistence.Interfaces.Implements.Global;
using Microsoft.EntityFrameworkCore;

namespace GHR.Persistence.Interfaces.Implements.Funcionarios
{
    public class FuncionarioPersistence : GlobalPersistence, IFuncionarioPersistence
    {
        private readonly GHRContext _context;

        public FuncionarioPersistence(GHRContext context) : base(context)
        {
            _context = context;
        }
        //Funcionarios
        public async Task<Funcionario[]> RecuperarFuncionariosAsync(int userId, string visao, bool incluirMetas = false)
        {
            IQueryable<Funcionario> query = _context.Funcionarios
                .Include(c => c.Cargos)
                .Include(d => d.Departamentos)
                .Include(u => u.Contas);

            if (incluirMetas)
            {
                query = query
                    .Include(fm => fm.FuncionariosMetas)
                    .ThenInclude(m => m.Meta);
            }

            query = query
                .AsNoTracking()
                .OrderBy(f => f.Id);

            if (visao.Contains("Funcionário") || !visao.Contains("RH")){
                query = query.Where(m => m.UserId == userId);
            }   
            

            return await query.ToArrayAsync();
        }

        public async Task<Funcionario[]> RecuperarFuncionariosPorNomeCompletoAsync(int userId, string visao, string nome, bool incluirMetas = false)
        {
            IQueryable<Funcionario> query = _context.Funcionarios
                .Include(c => c.Cargos)
                .Include(d => d.Departamentos)
                .Include(u => u.Contas);

            if (incluirMetas)
            {
                query = query
                    .Include(fm => fm.FuncionariosMetas)
                    .ThenInclude(m => m.Meta);
            }

            query = !visao.Contains("RH") 
                  ? query.Where(f => f.UserId == userId && f.Contas.NomeCompleto.ToLower().Contains(nome.ToLower())) 
                  : query.Where(f => f.Contas.NomeCompleto.ToLower().Contains(nome.ToLower()));

            query = query
                .AsNoTracking()
                .OrderBy(f => f.Id);


            return await query.ToArrayAsync();
        }
        public async Task<Funcionario> RecuperarFuncionarioPorIdAsync(int userId, string visao, int funcionarioId, bool incluirMetas = false)
        {
            IQueryable<Funcionario> query = _context.Funcionarios
                .Include(c => c.Cargos)
                .Include(d => d.Departamentos)
                .Include(u => u.Contas);

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
                .OrderBy(f => f.Id);

            return await query.FirstOrDefaultAsync();
        }
    }
}