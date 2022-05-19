namespace GHR.Domain.DataBase.Cargos
{
    public class Cargo
    {
        public int Id { get; set; }
        public string NomeCargo { get; set; }
        public string Funcao { get; set; }
        public int DepartamentoId { get; set; }
    }
}