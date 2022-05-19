using System.Linq;
using System.Threading.Tasks;
using GHR.Domain.DataBase.Funcionarios;
using GHR.Persistence.Interfaces.Contexts;
using GHR.Persistence.Interfaces.Contracts.Funcionarios;
using GHR.Persistence.Interfaces.Implements.Global;
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
        public async Task<FuncionarioMeta[]> RecuperarMetasPorFuncionarioIdAsync(int userId, string visao, int funcionarioId)
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
        public async Task<FuncionarioMeta> RecuperarFuncionarioMetaAsync(int userId, string visao, int funcionarioId, int metaId)
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