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
    public class FuncionarioMetaPersistence : GlobalPersistence, IFuncionarioMetaPersistence
    {
        private readonly GHRContext _context;

        public FuncionarioMetaPersistence(GHRContext context) : base (context)
        {
            _context = context;
        }
        //Funcionarios
        public async Task<PaginaLista<FuncionarioMeta>> RecuperarMetasPorFuncionarioIdAsync(int funcionarioId, PaginaParametros paginaParametros)
        {
            IQueryable<FuncionarioMeta> query = _context.FuncionariosMetas              
                .Include(f => f.Funcionarios)
                .Include(m => m.Metas);

                query = query
                    .AsNoTracking()
                    .OrderBy(fm => fm.MetaId)
                    .Where(fm => fm.FuncionarioId == funcionarioId &&
                                 (fm.Metas.Descricao.ToLower().Contains(paginaParametros.Termo.ToLower()) ||
                                  fm.Metas.NomeMeta.ToLower().Contains(paginaParametros.Termo.ToLower()))) ;

            return await PaginaLista<FuncionarioMeta>.CriarPaginaAsync(query, paginaParametros.NumeroDaPagina, paginaParametros.TamanhoDaPagina);
    
        }
        public async Task<FuncionarioMeta> RecuperarFuncionarioMetaAsync(int funcionarioId, int metaId)
        {
            IQueryable<FuncionarioMeta> query = _context.FuncionariosMetas
                .Include(m => m.Metas)
                .Include(f => f.Funcionarios);

            query = query
                    .AsNoTracking()
                    .OrderBy(fm => fm.MetaId)
                    .Where(fm => fm.FuncionarioId == funcionarioId &&
                                 fm.MetaId == metaId);

            return await query.FirstOrDefaultAsync();

        }
       
        public async Task<FuncionarioMeta[]> RecuperarFuncionariosMetasAsync()
        {
            IQueryable<FuncionarioMeta> query = _context.FuncionariosMetas
                .Include(m => m.Metas)
                .Include(f => f.Funcionarios);

            query = query
                    .AsNoTracking()
                    .OrderBy(fm => fm.MetaId);

            return await query.ToArrayAsync();

        }
    }
    
}