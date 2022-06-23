using System.Linq;
using GHR.Domain.DataBase.Cargos;
using GHR.Domain.DataBase.Contas;
using GHR.Domain.DataBase.Departamentos;
using GHR.Domain.DataBase.Empresas;
using GHR.Domain.DataBase.Funcionarios;
using GHR.Domain.DataBase.Metas;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GHR.Persistence.Interfaces.Contexts
{

    public class GHRContext : IdentityDbContext<Conta, 
                                                Funcao, 
                                                int,
                                                IdentityUserClaim<int>,
                                                ContaFuncao,
                                                IdentityUserLogin<int>,
                                                IdentityRoleClaim<int>,
                                                IdentityUserToken<int>>

    {
        public GHRContext(DbContextOptions<GHRContext> options) : base(options) { }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<DadoPessoal> DadosPessoais { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<FuncionarioMeta> FuncionariosMetas { get; set; }
        public DbSet<Meta> Metas { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<EmpresaConta> EmpresasContas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Empresa>(
                empresa =>
                {
                    empresa.HasIndex(e => e.MatrizId);
                });

            modelBuilder.Entity<ContaFuncao>(
                contaFuncao =>
                {
                    contaFuncao.HasKey(cf => new { cf.UserId, cf.RoleId });

                    contaFuncao.HasOne(cf => cf.Funcoes)
                        .WithMany(cf => cf.ContasFuncoes)
                        .HasForeignKey(cf => cf.RoleId)
                        .IsRequired();

                    contaFuncao.HasOne(cs => cs.Contas)
                        .WithMany(cfs => cfs.ContasFuncoes)
                        .HasForeignKey(c => c.UserId) 
                        .IsRequired();

                });

   
            modelBuilder.Entity<FuncionarioMeta>()
                    .HasKey(fm => new {  fm.FuncionarioId, fm.MetaId });
        }

    }
}