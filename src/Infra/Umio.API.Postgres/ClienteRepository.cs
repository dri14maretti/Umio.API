using Microsoft.EntityFrameworkCore;
using Umio.API.Application.CasosDeUso.Clientes.Inputs;
using Umio.API.Application.Contratos.Repositorios;
using Umio.API.Entities.Entidades;
using Umio.API.Postgres.Context;

namespace Umio.API.Postgres
{
    internal class ClienteRepository : IClienteRepository
    {
        private readonly UmioDbContext _context;
        public ClienteRepository(UmioDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CriarCliente(Cliente cliente)
        {
            var emailJaExiste = await _context.Clientes.AnyAsync(c => c.Email.ToLower() == cliente.Email.ToLower());
            if (emailJaExiste)
            {
                throw new InvalidOperationException("Já existe um cliente com este e-mail.");
            }

            var telefoneJaExiste = await _context.Clientes.AnyAsync(c => c.Telefone == cliente.Telefone);
            if (telefoneJaExiste)
            {
                throw new InvalidOperationException("Já existe um cliente com este telefone.");
            }

            _context.Clientes.Add(cliente);
            return await _context.SaveChangesAsync() >= 1;
        }

        public async Task<IEnumerable<Cliente>> ListarClientes(string? nome = null, string? email = null, Guid? id = null)
        {
            var query = _context.Clientes.AsQueryable();

            if (id.HasValue)
                query = query.Where(c => c.Id == id.Value);

            if (!string.IsNullOrWhiteSpace(nome))
                query = query.Where(c => EF.Functions.ILike(c.Nome, $"%{nome}%"));

            if (!string.IsNullOrWhiteSpace(email))
                query = query.Where(c => EF.Functions.ILike(c.Email, $"%{email}%"));

            return await query.ToListAsync();
        }


        public async Task<Cliente> BuscarClientePorId(Guid clienteId)
        {
            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(c => c.Id == clienteId);

            if (cliente == null)
            {
                throw new InvalidOperationException($"Cliente com ID {clienteId} não encontrado.");
            }
            return cliente;
        }

        public async Task<Cliente> AtualizarCliente(Cliente cliente)
        {

            var clienteExistente = await _context.Clientes
                .FirstOrDefaultAsync(c => c.Id == cliente.Id);

            if (clienteExistente == null)
            {
                throw new KeyNotFoundException($"Cliente com ID {cliente.Id} não encontrado.");
            }

            if (!string.IsNullOrWhiteSpace(cliente.Telefone))
            {
                var telefoneEmUso = await _context.Clientes
                    .AnyAsync(c => c.Telefone == cliente.Telefone && c.Id != cliente.Id);

                if (telefoneEmUso)
                    throw new InvalidOperationException("Este telefone já está em uso por outro cliente.");
            }

            if (!string.IsNullOrWhiteSpace(cliente.Email))
            {
                var emailEmUso = await _context.Clientes
                    .AnyAsync(c => c.Email.ToLower() == cliente.Email.ToLower() && c.Id != cliente.Id);

                if (emailEmUso)
                    throw new InvalidOperationException("Este e-mail já está em uso por outro cliente.");
            }

            clienteExistente.AtualizarCliente(cliente.Nome, cliente.Telefone, cliente.Email);
            _context.Clientes.Update(clienteExistente);

            await _context.SaveChangesAsync();
            return clienteExistente;
        }

        public async Task<bool> DeletarCliente(Guid clienteId)
        {
            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(c => c.Id == clienteId);

            if (cliente == null)
            {
                return false;
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Cliente?> BuscarPorEmail(string email)
        {
            return await _context.Clientes
                .FirstOrDefaultAsync(c => c.Email.ToLower() == email.ToLower());
        }
    }
}