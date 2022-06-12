using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHR.Domain;
using GHR.Domain.DataBase.Metas;
using GHR.Persistence.Interfaces.Contracts.Global;
using GHR.Persistence.Models;

namespace GHR.Persistence.Implements.Contracts.Metas
{
    public interface IMetaPersistence : IGlobalPersistence
    {
        //Metas
        Task<PaginaLista<Meta>> RecuperarMetasAsync( PaginaParametros paginaParametros, bool incluirFuncionarios = false);
        Task<Meta> RecuperarMetaPorIdAsync(int metaId,  bool incluirFuncionarios = false);   
        Task<Meta[]> RecuperarMetasAtivasAsync();
    }
}