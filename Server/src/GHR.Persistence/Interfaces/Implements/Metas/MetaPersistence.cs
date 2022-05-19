using System;
using System.Linq;
using System.Threading.Tasks;
using GHR.Domain.DataBase.Metas;
using GHR.Persistence.Implements.Contracts.Metas;
using GHR.Persistence.Interfaces.Contexts;
using GHR.Persistence.Interfaces.Implements.Global;
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
        public async Task<Meta> RecuperarMetaPorIdAsync(int metaId, bool incluirFuncionarios)
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

        public async Task<Meta[]> RecuperarMetasAsync(int userId, string visao, bool incluirFuncionarios = false)
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

            if (visao.Contains("Funcionário") || !visao.Contains("RH")){
                query = query.Where(m => m.UserId == userId);
            }   
            
            return await query.ToArrayAsync();
        }

        public async Task<Meta[]> RecuperarMetasPorDescricaoMetaAsync(int userId, string visao, string descricao, bool incluirFuncionarios = false)
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

            if (visao.Contains("Funcionário") || !visao.Contains("RH")){
                query = query.Where(m => m.UserId == userId);
            }   
            
            return await query.ToArrayAsync();
        }

        public async Task<Meta[]> RecuperarMetasPorMetaAprovadaAsync(int userId, string visao, bool metaAprovada, bool incluirFuncionarios)
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

            if (visao.Contains("Funcionário") || !visao.Contains("RH")){
                query = query.Where(m => m.UserId == userId);
            }   
            
            return await query.ToArrayAsync();
        }

        public async Task<Meta[]> RecuperarMetasPorMetaCumpridaAsync(int userId, string visao, bool metaCumprida, bool incluirFuncionarios)
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

            if (visao.Contains("Funcionário") || !visao.Contains("RH")){
                query = query.Where(m => m.UserId == userId);
            }   
            

            return await query.ToArrayAsync();
        }

        public async Task<Meta[]> RecuperarMetasPorNomeMetaAsync(int userId, string visao, string nome, bool incluirFuncionarios = true)
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

            if (visao.Contains("Funcionário") || !visao.Contains("RH")){
                query = query.Where(m => m.UserId == userId);
            }   

            return await query.ToArrayAsync();
        }
    }
}