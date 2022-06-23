using System;
using GHR.Domain.DataBase.Departamentos;
using GHR.Domain.DataBase.Empresas;

namespace GHR.Domain.DataBase.Cargos
{
    public class Cargo
    {
        public int Id { get; set; }
        public string NomeCargo { get; set; }
        public string Funcao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataEncerramento { get; set; }
        public int DepartamentoId { get; set; }
        public Departamento Departamentos { get; set; }
    }
}