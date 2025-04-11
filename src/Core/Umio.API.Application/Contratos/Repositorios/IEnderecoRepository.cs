using Umio.API.Entities.Entidades;

namespace Umio.API.Application.Contratos.Repositorios
{
    public interface IEnderecoRepository
    {
        public Task<bool> CriarEndereco(Endereco endereco);
    }
}
