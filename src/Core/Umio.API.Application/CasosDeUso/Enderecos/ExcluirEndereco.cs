using Umio.API.Application.CasosDeUso.Enderecos.Interfaces;
using Umio.API.Application.Contratos.Repositorios;
using Umio.API.Entities.Exceptions;

namespace Umio.API.Application.CasosDeUso.Enderecos
{
    internal class ExcluirEndereco : IExcluirEndereco
    {
        private readonly IEnderecoRepository _enderecoRepository;

        public ExcluirEndereco(IEnderecoRepository enderecoRepository)
        {
            _enderecoRepository = enderecoRepository;
        }

        public async Task<bool> Executar(Guid id)
        {
            await _enderecoRepository.ExcluirEndereco(id);

            return true; // Exemplo de retorno, substitua pela lógica real
        }
    }
}
