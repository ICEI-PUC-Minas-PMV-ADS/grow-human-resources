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
    public class MetaPersistence : IMetaPersistence
    {
        private readonly GHRContext _context;

        public MetaPersistence(GHRContext context)
        {
            _context = context;
        }

        //Metas
        public async Task<Meta> GetMetaByIdAsync(int metaId, bool incluirFuncionarios)
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

        public async Task<Meta[]> GetAllMetasAsync(bool incluirFuncionarios = false)
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

        public async Task<Meta[]> GetAllMetasByDescricaoMetaAsync(string descricao, bool incluirFuncionarios = false)
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

        public async Task<Meta[]> GetAllMetasByMetaAprovadaAsync(bool metaAprovada, bool incluirFuncionarios)
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

        public async Task<Meta[]> GetAllMetasByMetaCumpridaAsync(bool metaCumprida, bool incluirFuncionarios)
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

        public async Task<Meta[]> GetAllMetasByNomeMetaAsync(string nome, bool incluirFuncionarios = true)
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