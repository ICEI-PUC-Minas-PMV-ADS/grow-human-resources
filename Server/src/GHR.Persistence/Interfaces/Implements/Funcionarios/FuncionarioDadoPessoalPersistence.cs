using System.Linq;
using System.Threading.Tasks;
using GHR.Domain.DataBase.Funcionarios;
using GHR.Persistence.Interfaces.Contexts;
using GHR.Persistence.Interfaces.Contracts.Funcionarios;
using GHR.Persistence.Interfaces.Implements.Global;
using Microsoft.EntityFrameworkCore;

namespace GHR.Persistence.Interfaces.Implements.Funcionarios
{
    public class FuncionarioDadoPessoalPersistence : GlobalPersistence, IFuncionarioDadoPessoalPersistence
    {
        private readonly GHRContext _context;

        public FuncionarioDadoPessoalPersistence(GHRContext context) : base(context)
        {
            _context = context;
        }
        public async Task<DadoPessoal> RecuperarDadosPessoaisPorIdAsync(int id)
        {
            IQueryable<DadoPessoal> query = _context.DadosPessoais
                .AsNoTracking()
                .Where(dp => dp.Id == id );

            return await query.FirstOrDefaultAsync();
        }
    }
}