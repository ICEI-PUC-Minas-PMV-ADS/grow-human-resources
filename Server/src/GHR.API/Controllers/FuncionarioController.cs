using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHR.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GHR.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FuncionarioController : ControllerBase
    {
        public IEnumerable<Funcionario> _funcionario = new Funcionario[]{
            new Funcionario(){
                FuncionarioId = 1,
                NomeCompleto = "Alex Galdino",
                Email = "alex.galdino@ghr.com.br",
                Telefone = "(11)1111-1111",
                SetorId = 1,
                Salario = 1450,
                CargoId = 1,
                DataAdmissao = "01/01/2022",
                DataDemissao = "",
                ImagemURL = ""
            },
            new Funcionario(){
                FuncionarioId = 2,
                NomeCompleto = "Alex de Souza",
                Email = "alex.souza@ghr.com.br",
                Telefone = "(11)1111-1111",
                SetorId = 1,
                Salario = 2450,
                CargoId = 2,
                DataAdmissao = "01/01/2022",
                DataDemissao = "",
                ImagemURL = ""
            }
        };
        public FuncionarioController()
        {
        }

        [HttpGet]
        public IEnumerable<Funcionario> Get()
        {
            return _funcionario;
        }

        [HttpGet("{id}")]
        public IEnumerable<Funcionario> GetById(int id)
        {
            return _funcionario.Where(funcionario => funcionario.FuncionarioId == id);
        }
        [HttpPost]
        public string Post()
        {
            return "Exemplo de Post";
        }
        [HttpPut("{id}")]
        public string Put(int id)
        {
            return $"Exemplo de Put com id {id}";
        }
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            return $"Exemplo de delete com id {id}";
        }
    }
}
