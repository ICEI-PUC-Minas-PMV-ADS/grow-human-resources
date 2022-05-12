using System.Linq;
using System.Threading.Tasks;
using GHR.Domain;
using GHR.Persistence.Contexts;
using GHR.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GHR.Persistence.Context
{
    public class FuncionarioMetaPersistence : IFuncionarioMetaPersistence
    {
        private readonly GHRContext _context;

        public FuncionarioMetaPersistence(GHRContext context)
        {
            _context = context;
        }
        //Funcionarios
        public async Task<FuncionarioMeta[]> GetMetasByFuncionarioIdAsync(int funcionarioId)
        {
            IQueryable<FuncionarioMeta> query = _context.FuncionariosMetas               
                            .Include(f => f.Funcionario)
                            .Include(m => m.Meta);

            query = query
                    .AsNoTracking()
                    .OrderBy(fm => fm.MetaId)
                    .Where(fm => fm.FuncionarioId == funcionarioId);
            
            return await query.ToArrayAsync();
    
        }
        public async Task<FuncionarioMeta> GetFuncionarioMetaAsync(int funcionarioId, int metaId)
        {
            IQueryable<FuncionarioMeta> query = _context.FuncionariosMetas
                .Include(m => m.Meta)
                .Include(f => f.Funcionario);

            query = query
                    .AsNoTracking()
                    .OrderBy(fm => fm.MetaId)
                    .Where(fm => fm.FuncionarioId == funcionarioId &&
                                 fm.MetaId == metaId);

            return await query.FirstOrDefaultAsync();

        }
    }
}