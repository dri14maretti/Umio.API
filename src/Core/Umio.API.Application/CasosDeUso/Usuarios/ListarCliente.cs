
using Umio.API.Application.CasosDeUso.Usuarios.Interfaces;
using Umio.API.Entities.Entidades;

namespace Umio.API.Application.CasosDeUso.Usuarios
{
    public class ListarCliente : IListarCliente
    {
        private readonly List<Cliente> _clientes;
        public Task<IEnumerable<Cliente>> Execute(string? nome = null, string? email = null, string? telefone = null)
        {
            {
                var query = _clientes.AsQueryable();

                if (!string.IsNullOrEmpty(nome))
                    query = query.Where(c => c.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase));

                if (!string.IsNullOrEmpty(email))
                    query = query.Where(c => c.Email.Contains(email, StringComparison.OrdinalIgnoreCase));

                if (!string.IsNullOrEmpty(telefone))
                    query = query.Where(c => c.Telefone.Contains(telefone, StringComparison.OrdinalIgnoreCase));

                return Task.FromResult(query.AsEnumerable());
            }
        }
    }
}