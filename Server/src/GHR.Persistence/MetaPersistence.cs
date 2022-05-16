using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHR.Domain;
using GHR.Persistence.Contexts;
using GHR.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GHR.Persistence
{
    public class MetaPersistence : GlobalPersistence, IMetaPersistence
    {
        private readonly GHRContext _context;

        public MetaPersistence(GHRContext context) : base (context)
        {
            _context = context;
        }

        //Metas
        public async Task<Meta> GetMetaByIdAsync(int userId, string visao, int metaId, bool incluirFuncionarios)
        {
            IQueryable<Meta> query = _context.Metas;

            if (incluirFuncionarios)
            {
                query = query
                    .AsNoTracking()
                    .Include(fm => fm.FuncionariosMetas)
                    .ThenInclude(f => f.Funcionario);
            }

            query = query
                .AsNoTracking()
                .OrderBy(m => m.Id)
                .Where(m => m.Id == metaId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Meta[]> GetAllMetasAsync(int userId, string visao, bool incluirFuncionarios = false)
        {
            IQueryable<Meta> query = _context.Metas;

            if (incluirFuncionarios)
            {
                query = query
                    .Include(fm => fm.FuncionariosMetas)
                    .ThenInclude(f => f.Funcionario);
            }

            query = query
                .AsNoTracking()
                .OrderBy(m => m.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Meta[]> GetAllMetasByDescricaoMetaAsync(int userId, string visao, string descricao, bool incluirFuncionarios = false)
        {
            IQueryable<Meta> query = _context.Metas;

            if (incluirFuncionarios)
            {
                query = query
                    .Include(fm => fm.FuncionariosMetas)
                    .ThenInclude(f => f.Funcionario);
            }

            query = query
                .AsNoTracking()
                .OrderBy(m => m.Id)
                .Where(m => m.Descricao.ToLower().Contains(descricao.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Meta[]> GetAllMetasByMetaAprovadaAsync(int userId, string visao, bool metaAprovada, bool incluirFuncionarios)
        {
            IQueryable<Meta> query = _context.Metas;

            if (incluirFuncionarios)
            {
                query = query
                    .Include(fm => fm.FuncionariosMetas)
                    .ThenInclude(f => f.Funcionario);
            }

            query = query
                .AsNoTracking()
                .OrderBy(m => m.Id)
                .Where(m => m.MetaAprovada == metaAprovada);

            return await query.ToArrayAsync();
        }

        public async Task<Meta[]> GetAllMetasByMetaCumpridaAsync(int userId, string visao, bool metaCumprida, bool incluirFuncionarios)
        {
            IQueryable<Meta> query = _context.Metas;

            if (incluirFuncionarios)
            {
                query = query
                    .Include(fm => fm.FuncionariosMetas)
                    .ThenInclude(f => f.Funcionario);
            }

            query = query
                .AsNoTracking()
                .OrderBy(m => m.Id)
                .Where(m => m.MetaCumprida == metaCumprida);

            return await query.ToArrayAsync();
        }

        public async Task<Meta[]> GetAllMetasByNomeMetaAsync(int userId, string visao, string nome, bool incluirFuncionarios = true)
        {
            IQueryable<Meta> query = _context.Metas;

            if (incluirFuncionarios)
            {
                query = query
                    .Include(fm => fm.FuncionariosMetas)
                    .ThenInclude(f => f.Funcionario);
            }

            query = query
                .AsNoTracking()
                .OrderBy(m => m.Id)
                .Where(m => m.NomeMeta.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }
    }
}