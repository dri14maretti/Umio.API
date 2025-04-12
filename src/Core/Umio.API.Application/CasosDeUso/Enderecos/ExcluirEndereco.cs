using Umio.API.Application.CasosDeUso.Enderecos.Interfaces;
using Umio.API.Application.Contratos.Repositorios;

namespace Umio.API.Application.CasosDeUso.Enderecos
{
    internal class ExcluirEndereco : IExcluirEndereco
    {
        private readonly IEnderecoRepository _enderecoRepository;
        public async Task<bool> Executar(Guid id)
        {
            var endereco = await _enderecoRepository.BuscarEnderecoPorId(id);

            if(endereco == null)
            {
                throw new Exception("Endereço não encontrado");
            }

            await _enderecoRepository.ExcluirEndereco(id);

            return true; // Exemplo de retorno, substitua pela lógica real
        }
    }
}
