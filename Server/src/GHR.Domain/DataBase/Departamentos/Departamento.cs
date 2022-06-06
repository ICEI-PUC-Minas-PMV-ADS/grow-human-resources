using System;
using GHR.Domain.DataBase.Empresas;

namespace GHR.Domain.DataBase.Departamentos
{
    public class Departamento
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public Empresa Empresas { get; set; }
        public string NomeDepartamento { get; set; }
        public string SiglaDepartamento { get; set; }
        public string Diretor { get; set; }
        public string Gerente { get; set; }    
        public string Supervisor { get; set; }   
        public DateTime DataHoraCriacao { get; set; }   
        public DateTime DataHoraEncerramento { get; set; }
        public Boolean Ativo { get; set; }
    }
}