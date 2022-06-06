using System;
using System.Threading.Tasks;

using AutoMapper;

using GHR.Application.Dtos.Empresas;
using GHR.Application.Services.Contracts.Empresas;
using GHR.Domain.DataBase.Empresas;
using GHR.Persistence.Interfaces.Contracts.Empresas;
using GHR.Persistence.Interfaces.Contracts.Global;


namespace GHR.Application.Services.Implements.Empresas
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IGlobalPersistence _globalPersistence;
        private readonly IMapper _mapper;
        private readonly IEmpresaPersistence _empresaPersistence;

        public EmpresaService(
            IGlobalPersistence globalPersistence,
            IEmpresaPersistence empresaPersistence,
            IMapper mapper
        )
        {
            _globalPersistence = globalPersistence;
            _mapper = mapper;
            _empresaPersistence = empresaPersistence;
        }

        public async Task<EmpresaDto> AtualizarEmpresa(EmpresaDto empresaDto)
        {
            try
            {
                var empresa = await _empresaPersistence.RecuperarEmpresaPorIdAsync(empresaDto.Id);

                if (empresa == null) return null;
                    
               empresaDto.Id = empresa.Id;

                _mapper.Map(empresaDto, empresa);

                _empresaPersistence.Alterar<Empresa>(empresa);

                if (await _empresaPersistence.SalvarAsync()) {
                    var empresaRetorno = await _empresaPersistence.RecuperarEmpresaPorIdAsync(empresa.Id);

                    return _mapper.Map<EmpresaDto>(empresaRetorno);
                }

                return null;
            }
            catch (System.Exception ex)
            {

                throw new Exception($"Falha ao salvar empresa. Erro: {ex.Message}");
            }    
        }
        public async Task<EmpresaDto> CriarEmpresaAsync(EmpresaDto empresaDto)
        {
            try
            {
                var empresa = _mapper.Map<Empresa>(empresaDto);
                
                _globalPersistence.Cadastrar<Empresa>(empresa);

                if (await _globalPersistence.SalvarAsync()) {
                    var empresaRetorno = _mapper.Map<EmpresaDto>(empresa);
                    return empresaRetorno;
                }

                return null;
            }
            catch (System.Exception ex)
            {

                throw new Exception($"Falha ao criar empresa. Erro: {ex.Message}");
            }
        }

        public async Task<EmpresaDto> RecuperarEmpresaPorIdAsync(int empresaId)
        {
            try
            {
                var empresa = await _empresaPersistence.RecuperarEmpresaPorIdAsync(empresaId);

                if (empresa == null) return null;

                var empresaDto = _mapper.Map<EmpresaDto>(empresa);

                return empresaDto;
            }
            catch (System.Exception ex)
            {

                throw new Exception($"Falha ao recuperar conta por userId. Erro: {ex.Message}");
            }
        }
    
        public async Task<EmpresaDto[]> RecuperarEmpresasAsync()
        {
            try
            {
                var empresa = await _empresaPersistence.RecuperarEmpresasAsync();

                if (empresa == null) return null;

                var empresaDto = _mapper.Map<EmpresaDto[]>(empresa);

                return empresaDto;
            }
            catch (System.Exception ex)
            {

                throw new Exception($"Falha ao recuperar conta por userId. Erro: {ex.Message}");
            }
        }
    }
}