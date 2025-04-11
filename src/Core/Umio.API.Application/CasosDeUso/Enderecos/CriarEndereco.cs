using Umio.API.Application.CasosDeUso.Enderecos.Interfaces;
using Umio.API.Application.Contratos;
using Umio.API.Application.Contratos.Repositorios;
using Umio.API.Entities.Entidades;

namespace Umio.API.Application.CasosDeUso.Enderecos
{
    internal class CriarEndereco : ICriarEndereco
    {
        private readonly IEnderecoRepository _enderecoRepository;

        public CriarEndereco(IEnderecoRepository enderecoRepository)
        {
            _enderecoRepository = enderecoRepository;
        }
        public async Task<bool> Executar(CriarEnderecoRequest request)
        {
            var endereco = Endereco.CriarNovoEndereco(
                request.Cep, 
                request.Rua, 
                request.Bairro, 
                request.Cidade,
                request.Estado, 
                request.Numero, 
                request.Complemento,
                new Guid());

            return await _enderecoRepository.CriarEndereco(endereco);


        }
    }
}
