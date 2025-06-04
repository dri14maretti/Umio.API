using Umio.API.Application.CasosDeUso.Enderecos.Interfaces;
using Umio.API.Application.Contratos.Repositorios;
using Umio.API.Entities.Entidades;
using Umio.API.Entities.Exceptions;

namespace Umio.API.Application.CasosDeUso.Enderecos
{
    internal class BuscarEnderecosCliente : IBuscarEnderecosCliente
    {
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IClienteRepository _clienteRepository;
        public BuscarEnderecosCliente(IEnderecoRepository enderecoRepository, IClienteRepository clienteRepository)
        {
            _enderecoRepository = enderecoRepository;
            _clienteRepository = clienteRepository;
        }

        public async Task<IEnumerable<Endereco>> Executar(Guid clienteId)
        {
            var cliente = await _clienteRepository.BuscarClientePorId(clienteId);

            if (cliente == null)
            {
                throw new ExcecaoParametroIncorreto(clienteId.ToString(), "Não é possível buscar endereços de um cliente inválido");
            }

            var enderecos = await _enderecoRepository.BuscarEnderecosCliente(clienteId);

            return enderecos;
        }
    }
}
