using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GHR.Application.Dtos.Empresas;
using GHR.Application.Dtos.Funcionarios;

namespace GHR.Application.Dtos.Metas
{
    public class MetaDto
    {
        public int Id { get; set; }
                public int EmpresaId { get; set; }
        public EmpresaDto Empresas { get; set; }
        public string Supervisor { get; set; }
        [Display(Name = "Meta"),
        Required(ErrorMessage = "O campo {0} é obrigatório."),
        MinLength(4, ErrorMessage = "O campo {0} deve conter no mínimo 4 caracteres."),
        MaxLength(100, ErrorMessage = "O campo {0} deve conter no máximo 100 caracteres")]       
        public string NomeMeta { get; set; }

        [Display(Name = "Descricao Meta"),
        Required(ErrorMessage = "O campo {0} é obrigatório."),
        MinLength(30, ErrorMessage = "O campo {0} deve conter no mínimo 30 caracteres."),
        MaxLength(1500, ErrorMessage = "O campo {0} deve conter no máximo 1500 caracteres")]
        public string Descricao { get; set; }

        [Display(Name = "Meta Cumprida?"),
        Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public Boolean MetaCumprida { get; set; }

        [Display(Name = "Meta Arpovada?"),
        Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public Boolean MetaAprovada { get; set; }

        public DateTime? InicioPlanejado { get; set; }
        public DateTime? FimPlanejado { get; set; }
        public DateTime? InicioRealizado { get; set; }
        public DateTime? FimRealizado { get; set; }
        public IEnumerable<FuncionarioDto> FuncionariosDto { get; set; }
    }
}