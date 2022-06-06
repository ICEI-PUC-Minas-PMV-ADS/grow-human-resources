using System.Collections.Generic;
using System.Linq;
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
            IQueryable<Conta> query = _context.Users;

            query = query
                .Where(u => u.Id == id );

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Conta> RecuperarContaPorUserNameAsync(string userName)
        {
            IQueryable<Conta> query = _context.Users;

            query = query
                .Where(u => u.UserName == userName);    
            
            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Conta>> RecuperarContasAsync()
        {
            IQueryable<Conta> query = _context.Users;  
            
            return await query.ToListAsync();
        }
    }

}