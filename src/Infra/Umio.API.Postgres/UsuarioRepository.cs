using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Umio.API.Application.Contratos.Repositorios;
using Umio.API.Postgres.Context;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using Npgsql;
using System.Security.Cryptography;
using System.Text;
using Umio.API.Application.Contratos.Servicos;
using Umio.API.Entities.Entidades.Enums;


namespace Umio.API.Postgres
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly UmioDbContext _context;

        public UsuarioRepository(UmioDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CriarUsuario(Guid clienteId, string senha, Provedor provedor)
        {
            var senhaCriptografada = CryptgrafaSenha.CryptoSenha(senha);
            var provedorId = ObterIdProvedor(provedor);

            var query = "INSERT INTO usuario (Id, ProvedorId, ClienteId, Senha) VALUES (@Id, @ProvedorId, @ClienteId, @Senha)";

            var parameters = new[]
            {
        new NpgsqlParameter("@Id", Guid.NewGuid()),
        new NpgsqlParameter("@ProvedorId", provedorId),
        new NpgsqlParameter("@ClienteId", clienteId),
        new NpgsqlParameter("@Senha", senhaCriptografada)
    };

            await _context.Database.ExecuteSqlRawAsync(query, parameters);

            return true;
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