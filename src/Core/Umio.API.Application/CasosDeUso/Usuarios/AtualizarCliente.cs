using Umio.API.Application.CasosDeUso.Usuarios.Interfaces;
using Umio.API.Entities.Entidades;

namespace Umio.API.Application.CasosDeUso.Usuarios
{
    public class AtualizarCliente(List<Cliente> clientes) : IAtualizarCliente
    {
        private readonly List<Cliente> _clientes = clientes;

        public Task<Cliente?> Execute(Guid id, string? nome, string? telefone, string? fotoUrl)
        {
            var cliente = _clientes.FirstOrDefault(c => c.Id == id);
            if (cliente == null) return Task.FromResult<Cliente?>(null);

            if (!string.IsNullOrEmpty(nome)) cliente.Nome = nome;
            if (!string.IsNullOrEmpty(telefone)) cliente.Telefone = telefone;
            if (!string.IsNullOrEmpty(fotoUrl)) cliente.FotoUrl = fotoUrl;

            return Task.FromResult<Cliente?>(cliente);
        }
    }
}