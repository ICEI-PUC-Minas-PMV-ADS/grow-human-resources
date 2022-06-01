using System;
using System.Linq;
using System.Threading.Tasks;
using GHR.Domain.DataBase.Departamentos;
using GHR.Persistence.Interfaces.Contexts;
using GHR.Persistence.Interfaces.Contracts.Departamentos;
using GHR.Persistence.Interfaces.Implements.Global;
using GHR.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace GHR.Persistence.Interfaces.Implements.Departamentos
{
    public class DepartamentoPersistence : GlobalPersistence, IDepartamentoPersistence
    {

        private readonly GHRContext _context;

        public DepartamentoPersistence(GHRContext context) : base(context)
        {
            _context = context;
        }
        public async Task<PaginaLista<Departamento>> RecuperarDepartamentosAsync(PaginaParametros paginaParametros)
        {
            IQueryable<Departamento> query = _context.Departamentos;

            query = query
                .AsNoTracking()
                .OrderBy(d => d.Id)
                .Where(d => d.Id > 1 && 
                    (d.NomeDepartamento.ToLower().Contains(paginaParametros.Termo.ToLower()) ||
                     d.SiglaDepartamento.ToLower().Contains(paginaParametros.Termo.ToLower())));

            return await PaginaLista<Departamento>.CriarPaginaAsync(query, paginaParametros.NumeroDaPagina, paginaParametros.TamanhoDaPagina);
        }

        public async Task<Departamento> RecuperarDepartamentoPorIdAsync(int departamentoId)
        {
            IQueryable<Departamento> query = _context.Departamentos;

            query = query
                .AsNoTracking()
                .OrderBy(d => d.Id)
                .Where(d => d.Id == departamentoId && d.Id > 0);

            return await query.FirstOrDefaultAsync();
        }
    }
}