using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using GHR.Domain;
using Microsoft.EntityFrameworkCore;

namespace GHR.Persistence.Contexts
{
    public class GHRContext : DbContext
    {
        public GHRContext(DbContextOptions<GHRContext> options) : base(options) { }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<FuncionarioMeta> FuncionariosMetas { get; set; }
        public DbSet<Login> Login { get; set; }
        public DbSet<Meta> Metas { get; set; }
        public DbSet<Supervisor> Supervisores { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FuncionarioMeta>()
                .HasKey(FM => new { FM.FuncionarioId, FM.MetaId });
        }
    }
}