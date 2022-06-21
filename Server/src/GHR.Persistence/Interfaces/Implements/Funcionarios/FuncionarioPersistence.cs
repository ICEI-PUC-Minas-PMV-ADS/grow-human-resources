using System.Linq;
using System.Threading.Tasks;
using GHR.Domain.DataBase.Funcionarios;
using GHR.Persistence.Interfaces.Contexts;
using GHR.Persistence.Interfaces.Contracts.Funcionarios;
using GHR.Persistence.Interfaces.Implements.Global;
using GHR.Persistence.Models;
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
        public async Task<PaginaLista<Funcionario>> RecuperarFuncionariosAsync(PaginaParametros paginaParametros, bool incluirMetas = true)
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
                    .ThenInclude(m => m.Metas);
            }

            query = query
                .AsNoTracking()
                .Where(f => f.Id > 1 && 
                            (f.Contas.NomeCompleto.ToLower().Contains(paginaParametros.Termo.ToLower()) ||
                             f.Contas.Email.ToLower().Contains(paginaParametros.Termo.ToLower()) ||
                             f.Contas.PhoneNumber.ToLower().Contains(paginaParametros.Termo.ToLower())))
                .OrderBy(f => f.Id);

            return await PaginaLista<Funcionario>.CriarPaginaAsync(query, paginaParametros.NumeroDaPagina, paginaParametros.TamanhoDaPagina); 
        }

        public async Task<Funcionario> RecuperarFuncionarioPorIdAsync(int funcionarioId, bool incluirMetas = false)
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
                    .ThenInclude(m => m.Metas);
            }

            query = query
                .AsNoTracking()
                .Where(f => f.Id > 1 && f.Id == funcionarioId )
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
                .Where(f => f.ContaId == contaId )
                .OrderBy(f => f.Id);

            return await query.FirstOrDefaultAsync();
        }
        
        public async Task<Funcionario[]> RecuperarFuncionarioPorDepartamentoIdAsync(int departamentoId)
        {
            IQueryable<Funcionario> query = _context.Funcionarios
                .Include(ca => ca.Cargos)
                .Include(d => d.Departamentos)
                .Include(co => co.Contas)
                .Include(e => e.Enderecos)
                .Include(dp => dp.DadosPessoais)
                .AsNoTracking()
                .Where(f => f.Id > 1 && f.DepartamentoId == departamentoId)
                .OrderBy(f => f.Id);

            return await query.ToArrayAsync();
        }
    }
}