using Umio.API.Entities.Entidades;

namespace Umio.API.Application.Contratos.Repositorios
{
    public interface IEnderecoRepository
    {
        public Task<bool> CriarEndereco(Endereco endereco);
        public Task<IEnumerable<Endereco>> BuscarEnderecosCliente(Guid clienteId);
        public Task<Endereco> BuscarEnderecoPorId(Guid id);
        public Task<bool> ExcluirEndereco(Guid id);
    }
}
