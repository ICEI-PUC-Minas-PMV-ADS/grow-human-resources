using System;
using System.Linq;
using System.Threading.Tasks;
using GHR.Persistence.Interfaces.Contexts;
using GHR.Persistence.Interfaces.Contracts.Global;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace GHR.Persistence.Interfaces.Implements.Global
{
    public class GlobalPersistence : IGlobalPersistence
    {
        private readonly GHRContext _context;

        public GlobalPersistence(GHRContext context)
        {
            _context = context;
        }
         public void Cadastrar<T>(T entity) where T : class
         
        {

            _context.Add(entity);
        }

        public void Alterar<T>(T entity) where T : class

        {

            _context.Update(entity);

        }
        public void Excluir<T>(T entity) where T : class
        {

            _context.Remove(entity);
        }

        public void ExcluirIntervalo<T>(T[] entityArray) where T : class
        {

             _context.RemoveRange(entityArray);
        }
        public async Task<bool> SalvarAsync()
        {

            return (await _context.SaveChangesAsync()) > 0;
        }
        
        
    }
}