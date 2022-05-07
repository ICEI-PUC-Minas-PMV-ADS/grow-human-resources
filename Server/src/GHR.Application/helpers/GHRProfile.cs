using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GHR.Application.Dtos;
using GHR.Domain;

namespace GHR.Application.helpers
{
    public class GHRProfile : Profile
    {
        public GHRProfile() 
        {
            CreateMap<Cargo, CargoDto>().ReverseMap();
            CreateMap<Departamento, DepartamentoDto>().ReverseMap();
            CreateMap<Funcionario, FuncionarioDto>().ReverseMap();
            CreateMap<Login, LoginDto>().ReverseMap();
            CreateMap<Meta, MetaDto>().ReverseMap();
            CreateMap<Supervisor, SupervisorDto>().ReverseMap();
        }
    }
}