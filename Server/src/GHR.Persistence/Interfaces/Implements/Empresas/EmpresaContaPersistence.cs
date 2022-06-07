using System.Linq;
using System.Threading.Tasks;
using GHR.Domain.DataBase.Empresas;
using GHR.Persistence.Interfaces.Contexts;
using GHR.Persistence.Interfaces.Contracts.Empresas;
using GHR.Persistence.Interfaces.Implements.Global;
using Microsoft.EntityFrameworkCore;

namespace GHR.Persistence.Interfaces.Implements.Empresas
{
    public class EmpresaContaPersistence : GlobalPersistence, IEmpresaContaPersistence
    {
        private readonly GHRContext _context;

        public EmpresaContaPersistence(GHRContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Empresa> RecuperarEmpresaPorIdAsync(int id)
        {
            IQueryable<Empresa> query = _context.Empresas;

            query = query
                .Where(e => e.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<EmpresaConta> RecuperarEmpresaContaPorEmpresaIdUserNameAsync(int empresaId, string userName)
        {
            IQueryable<EmpresaConta> query = _context.EmpresasContas;

            query = query
                .AsNoTracking()
                .Where(e => e.EmpresaId == empresaId && e.UserName == userName);

            return await query.FirstOrDefaultAsync();
        }
    }
}