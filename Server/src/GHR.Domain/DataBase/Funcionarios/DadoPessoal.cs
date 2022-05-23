namespace GHR.Domain.DataBase.Funcionarios
{
    public class DadoPessoal
    {
        public int Id { get; set; } 
        public string CPF { get; set; }
        public string TituloEleitor { get; set; }  
        public string Identidade { get; set; }
        public string DataExpedicaoIdentidade { get; set; }     
        public string OrgaoExpedicaoIdentidade { get; set; }
        public string UfIdentidade { get; set; }
        public string EstadoCivil { get; set; } 
        public string CarteiraTrabalho { get; set; }
        public string DataExpedicaoCarteiraTrabalho { get; set; } 
    }
}