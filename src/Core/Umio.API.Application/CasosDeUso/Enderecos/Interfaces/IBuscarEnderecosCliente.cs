using Umio.API.Entities.Entidades;

namespace Umio.API.Application.CasosDeUso.Enderecos.Interfaces
{
    public interface IBuscarEnderecosCliente
    {
        public Task<IEnumerable<Endereco>> Executar(Guid clienteId);
    }
}
