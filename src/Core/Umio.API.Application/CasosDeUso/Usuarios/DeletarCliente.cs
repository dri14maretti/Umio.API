using Umio.API.Application.CasosDeUso.Usuarios.Interfaces;
using Umio.API.Entities.Entidades;

namespace Umio.API.Application.CasosDeUso.Usuarios
{
    public class DeletarCliente : IDeletarCliente
    {
        private readonly List<Cliente> _clientes;

        public DeletarCliente(List<Cliente> clientes)
        {
            _clientes = clientes;
        }

        public Task<bool> Execute(Guid id)
        {
            var cliente = _clientes.FirstOrDefault(c => c.Id == id);
            if (cliente == null) return Task.FromResult(false);

            _clientes.Remove(cliente);
            return Task.FromResult(true);
        }
    }
}