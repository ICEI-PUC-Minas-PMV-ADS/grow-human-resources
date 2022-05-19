using AutoMapper;
using GHR.Application.Dtos.Cargos;
using GHR.Application.Dtos.Contas;
using GHR.Application.Dtos.Departamentos;
using GHR.Application.Dtos.Funcionarios;
using GHR.Application.Dtos.Metas;
using GHR.Domain.DataBase.Cargos;
using GHR.Domain.DataBase.Contas;
using GHR.Domain.DataBase.Departamentos;
using GHR.Domain.DataBase.Funcionarios;
using GHR.Domain.DataBase.Metas;

namespace GHR.Application.helpers
{
    public class GHRProfile : Profile
    {
        public GHRProfile() 
        {
            CreateMap<Cargo, CargoDto>().ReverseMap();
            CreateMap<Departamento, DepartamentoDto>().ReverseMap();
            CreateMap<Funcionario, FuncionarioDto>().ReverseMap();
            CreateMap<Meta, MetaDto>().ReverseMap();
            CreateMap<FuncionarioMeta, FuncionarioMetaDto>().ReverseMap();
            
            CreateMap<Conta, ContaDto>().ReverseMap();
            CreateMap<Conta, ContaLoginDto>().ReverseMap();
            CreateMap<Conta, ContaAtualizarDto>().ReverseMap();
        }
    }
}