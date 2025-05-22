using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;
using Umio.API.Application.Contratos.Repositorios;
using Umio.API.Entities.Entidades;
using Umio.API.Entities.Exceptions;
using Umio.API.Postgres.Context;

namespace Umio.API.Postgres
{
    class EnderecoRepository : IEnderecoRepository
    {
        private readonly UmioDbContext _context;
        public EnderecoRepository(UmioDbContext context)
        {
            _context = context;
        }
        public async Task<Endereco> BuscarEnderecoPorId(Guid id)
        {
            var endereco = await _context.Enderecos
                .FirstOrDefaultAsync(e => e.Id == id);

            return endereco;
        }

        public async Task<IEnumerable<Endereco>> BuscarEnderecosCliente(Guid clienteId)
        {
            var enderecos = await _context.Enderecos
                .Where(e => e.ClienteId == clienteId)
                .ToListAsync();

            return enderecos;
        }

        public async Task<bool> CriarEndereco(Endereco endereco)
        {
            var enderecoJaExiste = await _context.Enderecos.AnyAsync(e => e.Cep == endereco.Cep && e.ClienteId == endereco.ClienteId);
            if (enderecoJaExiste)
            {
                throw new ExcecaoPropriedadeInvalida(nameof(endereco.Cep), "Já existe um endereço com este CEP para este cliente.");
            }

            _context.Enderecos.Add(endereco);
            return await _context.SaveChangesAsync() >= 1;
        }

        public async Task<bool> ExcluirEndereco(Guid id)
        {
            var endereco = await _context.Enderecos
                .FirstOrDefaultAsync(e => e.Id == id && e.Ativo);
            if (endereco == null)
            {
                throw new ExcecaoParametroIncorreto(id.ToString());
            }
            endereco.DesativarEndereco();
            _context.Enderecos.Update(endereco);
            return await _context.SaveChangesAsync() >= 1;
        }
    }
}
