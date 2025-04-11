using Umio.API.Application.Contratos.Repositorios;
using Umio.API.Entities.Entidades;

namespace Umio.API.Postgres
{
    class EnderecoRepository : IEnderecoRepository
    {
        public async Task<bool> CriarEndereco(Endereco endereco)
        {
            return true;
        }
    }
}
