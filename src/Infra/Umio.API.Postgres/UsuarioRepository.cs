using Microsoft.EntityFrameworkCore;
using Npgsql;
using Umio.API.Application.Contratos.Repositorios;
using Umio.API.Application.Contratos.Servicos;
using Umio.API.Entities.Entidades;
using Umio.API.Entities.Entidades.Enums;
using Umio.API.Postgres.Context;


namespace Umio.API.Postgres
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly UmioDbContext _context;

        public UsuarioRepository(UmioDbContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<Usuario>> BuscarPorClienteId(Guid clienteId)
        {
            throw new NotImplementedException();
        }

        public async Task<Usuario> BuscarPorClienteIdProvedorId(Guid clienteId, Provedor provedor)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.ClienteId == clienteId && u.Provedor == provedor);

            return usuario;
        }

        public async Task<bool> CriarUsuario(Usuario usuario)
        {
            //if (BuscarPorClienteIdProvedorId(usuario.ClienteId, usuario.Provedor) != null) throw new Exception();

            _context.Usuarios.Add(usuario);
            return await _context.SaveChangesAsync() >= 1;
        }

        public async Task<bool> DeletarPorClienteId(Guid clienteId)
        {
            var query = "DELETE FROM usuario WHERE ClienteId = @ClienteId";

            var parametro = new NpgsqlParameter("@ClienteId", clienteId);

            await _context.Database.ExecuteSqlRawAsync(query, parametro);

            return true;
        }

        private int ObterIdProvedor(Provedor provedor)
        {
            switch (provedor)
            {
                case Provedor.Umio:
                    return 0;
                case Provedor.Google:
                    return 1;
                case Provedor.Apple:
                    return 2;
                default:
                    throw new ArgumentException("Provedor inválido.");
            }
        }
    }

}