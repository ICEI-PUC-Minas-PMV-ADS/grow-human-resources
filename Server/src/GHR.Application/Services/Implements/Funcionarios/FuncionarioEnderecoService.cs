

using System;
using System.Threading.Tasks;
using AutoMapper;
using GHR.Application.Dtos.Funcionarios;
using GHR.Application.Services.Contracts.Funcionarios;
using GHR.Domain.DataBase.Funcionarios;
using GHR.Persistence.Interfaces.Contracts.Funcionarios;
using GHR.Persistence.Interfaces.Contracts.Global;

namespace GHR.Application.Services.Implements.Funcionarios
{
    public class FuncionarioEnderecoService : IFuncionarioEnderecoService
    {
        private readonly IGlobalPersistence _globalPersistence;
        private readonly IFuncionarioEnderecoPersistence _funcionarioEnderecoPersistence;
        private readonly IMapper _mapper;

        public FuncionarioEnderecoService(
            IGlobalPersistence globalPersistence, 
            IFuncionarioEnderecoPersistence funcionarioEnderecoPersistence,
            IMapper mapper)
        {
            _globalPersistence = globalPersistence;
            _funcionarioEnderecoPersistence = funcionarioEnderecoPersistence;
            _mapper = mapper;
        }
        public async Task<EnderecoDto> CriarEnderecoAsync(EnderecoDto model)
        {
            try
            {
                var endereco = _mapper.Map<Endereco>(model);

                _globalPersistence.Cadastrar<Endereco>(endereco);

                if (await _globalPersistence.SalvarAsync())
                {
                    var enderecoRetorno = await _funcionarioEnderecoPersistence
                        .RecuperarEnderecoPorIdAsync(endereco.Id);

                    return _mapper.Map<EnderecoDto>(enderecoRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<EnderecoDto> AlterarEnderecoAsync(int enderecoId, EnderecoDto model)
        {
            try
            {
                var endereco = await _funcionarioEnderecoPersistence
                    .RecuperarEnderecoPorIdAsync(enderecoId);

                if (endereco == null) return null;

                model.Id = endereco.Id;
                
                _mapper.Map(model, endereco);

                _globalPersistence.Alterar<Endereco>(endereco);

                if (await _globalPersistence.SalvarAsync())
                {
                    var enderecoRetorno = await _funcionarioEnderecoPersistence
                        .RecuperarEnderecoPorIdAsync( endereco.Id);

                    return _mapper.Map<EnderecoDto>(enderecoRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> ExcluirEnderecoAsync(int enderecoId)
        {
            try
            {
                var endereco = await _funcionarioEnderecoPersistence
                    .RecuperarEnderecoPorIdAsync(enderecoId);

                if (endereco == null) throw new Exception("Dado Pessoal não encontrado para exclusão");
                

                _globalPersistence.Excluir<Endereco>(endereco);

                return await _globalPersistence.SalvarAsync();
            }

            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

           } 

        public async Task<EnderecoDto> RecuperarEnderecoPorIdAsync(int enderecoId)
        {
            try
            {
                var endereco = await _funcionarioEnderecoPersistence
                    .RecuperarEnderecoPorIdAsync(enderecoId);

                if (endereco == null) return null;

                var enderecoMapper = _mapper.Map<EnderecoDto>(endereco);

                return enderecoMapper;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

    }
}