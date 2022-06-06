using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHR.Domain.DataBase.Metas;
using GHR.Persistence.Implements.Contracts.Metas;
using GHR.Persistence.Interfaces.Contexts;
using GHR.Persistence.Interfaces.Implements.Global;
using GHR.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace GHR.Persistence.Interfaces.Implements.Metas
{
    public class MetaPersistence : GlobalPersistence, IMetaPersistence
    {
        private readonly GHRContext _context;

        public MetaPersistence(GHRContext context) : base (context)
        {
            _context = context;
        }

        //Metas
        public async Task<Meta> RecuperarMetaPorIdAsync(int metaId, int empresaId, bool incluirFuncionarios)
        {
            IQueryable<Meta> query = _context.Metas
                .Include(e => e.Empresas);

            if (incluirFuncionarios)
            {
                query = query
                    .AsNoTracking()
                    .Include(fm => fm.FuncionariosMetas)
                    .ThenInclude(f => f.Funcionarios);
            }

            query = query
                .AsNoTracking()
                .OrderBy(m => m.Id)
                .Where(m => m.Id == metaId && m.EmpresaId == empresaId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<PaginaLista<Meta>> RecuperarMetasAsync(int empresaId, PaginaParametros paginaParametros, bool incluirFuncionarios = false)
        {
            IQueryable<Meta> query = _context.Metas
                .Include(e => e.Empresas);

            if (incluirFuncionarios)
            {
                query = query
                    .Include(fm => fm.FuncionariosMetas)
                    .ThenInclude(f => f.Funcionarios);
            }

            query = query
                .AsNoTracking()
                .Where(m => m.EmpresaId == empresaId &&
                            (m.Descricao.ToLower().Contains(paginaParametros.Termo.ToLower()) ||
                             m.NomeMeta.ToLower().Contains(paginaParametros.Termo.ToLower())))
                .OrderBy(m => m.Id);

            return await PaginaLista<Meta>.CriarPaginaAsync(query, paginaParametros.NumeroDaPagina, paginaParametros.TamanhoDaPagina);
        }
        
        public async Task<Meta[]> RecuperarMetasAtivasAsync(int empresaId)
        {
            IQueryable<Meta> query = _context.Metas;

            query = query
                .AsNoTracking()
                .Where(m => m.MetaAprovada && !m.MetaCumprida && m.EmpresaId == empresaId)
                .OrderBy(m => m.Id);

            return await query.ToArrayAsync();
        }
    }
}
    