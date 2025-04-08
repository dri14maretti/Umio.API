using Umio.API.Entities.Entidades;

namespace Umio.API.Application.Contratos
{
    public interface ICepService
    {
        public Task<Endereco> BuscarEnderecoPorCep(string cep);
    }
}
