using System.Collections.Generic;
using System.Threading.Tasks;

using GHR.Domain.DataBase.Contas;
using GHR.Persistence.Interfaces.Contexts;
using GHR.Persistence.Interfaces.Contracts.Contas;
using GHR.Persistence.Interfaces.Implements.Global;

using Microsoft.EntityFrameworkCore;

namespace GHR.Persistence.Interfaces.Implements.Contas
{
    public class ContaPersistence : GlobalPersistence, IContaPersistence
    {
        private readonly GHRContext _context;

        public ContaPersistence(GHRContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Conta> RecuperarContaPorIdAsync(int id)
        {
            return await _context.Users
                                 .FindAsync(id);
        }

        public async Task<Conta> RecuperarContaPorUserNameAsync(string userName)
        {
            return await _context.Users
                                 .SingleOrDefaultAsync(
                                     user => user.UserName == userName.ToLower()
                                 );
        }

        public async Task<IEnumerable<Conta>> RecuperarContasAsync()
        {
            return await _context.Users
                                 .ToListAsync();
        }
    }

}