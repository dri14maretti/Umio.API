using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;
using Umio.API.Entities.Entidades.Enums;

namespace Umio.API.Entities.Entidades
{
    [Table("usuario")]
    public class Usuario
    {
        [Column("id")]
        public Guid Id { get; private set; }
        [Column("clienteid")]
        public Guid ClienteId { get; private set; }
        [Column("senha")]
        public string Senha { get; private set; }
        [Column("provedorid")]
        public Provedor Provedor { get; private set; }

        private Usuario(string senha, Guid clienteId, Provedor provedor)
        {
            Id = Guid.NewGuid();
            Senha = senha;
            ClienteId = clienteId;
            Provedor = provedor;
        }

        public Usuario(Guid id, string senha, Guid clienteId, Provedor provedor)
        {
            Id = id;
            Senha = senha;
            ClienteId = clienteId;
            Provedor = provedor;
        }

        public static Usuario CriarNovoUsuario(string senha, Guid clienteId, Provedor provedor)
        {
            if (!SenhaForte(senha))

                throw new ArgumentException("A senha deve conter pelo menos 6 caracteres, incluindo letras maiúsculas, minúsculas, números e caracteres especiais.", nameof(senha));


            var senhaCriptografada = CryptoSenha(senha);

            return new Usuario(senhaCriptografada, clienteId, provedor);
        }

        public static Usuario CriarUsuarioOAuth(Guid clienteId, Provedor provedor)
        {
            return new Usuario(Guid.NewGuid(), "", clienteId, provedor);
        }


        private static bool SenhaForte(string senha)
        {
            return !string.IsNullOrWhiteSpace(senha) &&
            senha.Length >= 6 &&
            senha.Any(char.IsUpper) &&
            senha.Any(char.IsLower) &&
            senha.Any(char.IsDigit) &&
            senha.Any(c => !char.IsLetterOrDigit(c));
        }

        public bool ValidarSenha(string senha)
        {
            var senhaCriptografada = CryptoSenha(senha);

            return senhaCriptografada == Senha;
        }

        private static string CryptoSenha(string senha)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] senhaBytes = Encoding.UTF8.GetBytes(senha);
                byte[] hashBytes = sha256.ComputeHash(senhaBytes);

                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}
