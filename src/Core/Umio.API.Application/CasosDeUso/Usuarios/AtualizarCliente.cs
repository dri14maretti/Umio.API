using Umio.API.Application.CasosDeUso.Usuarios.Interfaces;
using Umio.API.Entities.Entidades;

namespace Umio.API.Application.CasosDeUso.Usuarios
{
    public class AtualizarCliente(List<Cliente> clientes) : IAtualizarCliente
    {
        private readonly List<Cliente> _clientes = clientes;

        public async Task<Cliente?> Executar(Guid id, string? nome, string? telefone)
        {
            var cliente = _clientes.FirstOrDefault(c => c.Id == id);
            if (cliente == null) return null;

            cliente.AtualizarCliente(nome, telefone);   

            return cliente;
        }
    }
}