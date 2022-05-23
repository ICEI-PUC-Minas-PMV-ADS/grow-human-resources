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
        public async Task<Funcionario[]> RecuperarFuncionariosAsync(int userId, string visao, bool incluirMetas = false)
        {
            IQueryable<Funcionario> query = _context.Funcionarios
                .Include(ca => ca.Cargos)
                .Include(d => d.Departamentos)
                .Include(co => co.Contas)
                .Include(e => e.Enderecos)
                .Include(dp => dp.DadosPessoais);

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

        public async Task<Funcionario[]> RecuperarFuncionariosPorNomeCompletoAsync(int userId, string visao, string nome, bool incluirMetas = false)
        {
            IQueryable<Funcionario> query = _context.Funcionarios
                .Include(ca => ca.Cargos)
                .Include(d => d.Departamentos)
                .Include(co => co.Contas)
                .Include(e => e.Enderecos)
                .Include(dp => dp.DadosPessoais);

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
        public async Task<Funcionario> RecuperarFuncionarioPorIdAsync(int userId, string visao, int funcionarioId, bool incluirMetas = false)
        {
            IQueryable<Funcionario> query = _context.Funcionarios
                .Include(ca => ca.Cargos)
                .Include(d => d.Departamentos)
                .Include(co => co.Contas)
                .Include(e => e.Enderecos)
                .Include(dp => dp.DadosPessoais);

            if (incluirMetas)
            {
                query = query
                    .Include(fm => fm.FuncionariosMetas)
                    .ThenInclude(m => m.Meta);
            }

            query = query
                .AsNoTracking()
                .OrderBy(f => f.Id);

            return await query.FirstOrDefaultAsync();
        }
        public async Task<Funcionario> RecuperarFuncionarioPorContaIdAsync(int contaId)
        {
            IQueryable<Funcionario> query = _context.Funcionarios
                .Include(ca => ca.Cargos)
                .Include(d => d.Departamentos)
                .Include(co => co.Contas)
                .Include(e => e.Enderecos)
                .Include(dp => dp.DadosPessoais)
                .AsNoTracking()
                .Where(f => f.UserId == contaId)
                .OrderBy(f => f.Id);

            return await query.FirstOrDefaultAsync();
        }
    }
}