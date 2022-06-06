using System.Linq;
using System.Threading.Tasks;
using GHR.Domain.DataBase.Funcionarios;
using GHR.Persistence.Interfaces.Contexts;
using GHR.Persistence.Interfaces.Contracts.Funcionarios;
using GHR.Persistence.Interfaces.Implements.Global;
using Microsoft.EntityFrameworkCore;

namespace GHR.Persistence.Interfaces.Implements.Funcionarios
{
    public class FuncionarioEnderecoPersistence : GlobalPersistence, IFuncionarioEnderecoPersistence
    {
        private readonly GHRContext _context;

        public FuncionarioEnderecoPersistence(GHRContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Endereco> RecuperarEnderecoPorIdAsync(int id, int funcionarioId, int empresaId)
        {
            IQueryable<Endereco> query = _context.Enderecos
                .Include(e => e.Empresas)
                .Where(e => e.Id == id && e.EmpresaId == empresaId && e.FuncionarioId == funcionarioId)
                .AsNoTracking();

            return await query.FirstOrDefaultAsync();
        }
    }
}