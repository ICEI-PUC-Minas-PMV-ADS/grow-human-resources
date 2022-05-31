using GHR.Domain.DataBase.Funcionarios;

namespace GHR.Domain.DataBase.Departamentos
{
    public class Departamento
    {
        public int Id { get; set; }
        public string NomeDepartamento { get; set; }
        public string SiglaDepartamento { get; set; }
        public int? MetaId { get; set; }
        public string Diretor { get; set; }
        public string Gerente { get; set; }    
        public string Supervisor { get; set; }      
    }
}