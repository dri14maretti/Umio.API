using Umio.API.Application.CasosDeUso.Enderecos.Interfaces;
using Umio.API.Application.Contratos;
using Umio.API.Application.Contratos.Repositorios;
using Umio.API.Entities.Entidades;

namespace Umio.API.Application.CasosDeUso.Enderecos
{
    internal class CriarEndereco : ICriarEndereco
    {
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IClienteRepository _clienteRepository;

        public CriarEndereco(IEnderecoRepository enderecoRepository, IClienteRepository clienteRepository)
        {
            _enderecoRepository = enderecoRepository;
            _clienteRepository = clienteRepository;
        }
        public async Task<bool> Executar(CriarEnderecoRequest request, Guid clienteId)
        {
            var cliente = _clienteRepository.BuscarClientePorId(clienteId);

            if(cliente == null)
            {
                throw new Exception("Não é possível criar um endereço sem um usuário válido atrelado");
            }

            var endereco = Endereco.CriarNovoEndereco(
                request.Cep, 
                request.Rua, 
                request.Bairro, 
                request.Cidade,
                request.Estado, 
                request.Numero, 
                request.Complemento,
                Guid.NewGuid());

            return await _enderecoRepository.CriarEndereco(endereco);


        }
    }
}
