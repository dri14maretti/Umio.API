using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using Umio.API.Entities.Exceptions;

namespace Umio.API.Entities.Entidades
{
    [Table("cliente")]
    public class Cliente
    {
        [Key]
        [Column("id")]
        public Guid Id { get; private set; }
        [Column("nome")]
        public string Nome { get; private set; }
        [Column("email")]
        public string Email { get; private set; }
        [Column("telefone")]
        public string Telefone { get; private set; }
        [Column("pontos")]
        public int Pontos { get; private set; }
        public List<Endereco> Enderecos { get; private set; } = [];
        public List<Pedido> Pedidos { get; private set; } = [];

        public Cliente(string nome, string email, string telefone)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Email = ValidarEmail(email);
            Telefone = ValidarTelefone(telefone);
            Pontos = 0;
        }
        private static string ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email não pode ser vazio");

            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (!emailRegex.IsMatch(email))
                throw new ArgumentException("Email inválido");

            return email;
        }

        private static string ValidarTelefone(string telefone)
        {
            var apenasNumeros = new string(telefone.Where(char.IsDigit).ToArray());

            if (apenasNumeros.Length < 11)
                throw new ExcecaoPropriedadeInvalida(nameof(telefone));

            return apenasNumeros;
        }
        public void AdicionarEndereco(Endereco endereco)
        {
            if (endereco == null)
                throw new ArgumentNullException(nameof(endereco), "Endereço não pode ser nulo");

            Enderecos.Add(endereco);
        }
        public void AdicionarPontos(int pontos)
        {
            if (pontos <= 0)
                throw new ArgumentException("Pontos devem ser positivos");

            Pontos += pontos;
        }
        public void AtualizarCliente(string? nome = null, string? telefone = null, string? email = null, int? pontos = null)
        {
            if (!string.IsNullOrWhiteSpace(nome))
                Nome = nome;

            if (!string.IsNullOrWhiteSpace(telefone))

                Telefone = ValidarTelefone(telefone);

            if (!string.IsNullOrWhiteSpace(email))
                Email = ValidarEmail(email);

            if (pontos.HasValue)
                Pontos = pontos.Value;
        }

        public static bool SenhaForte(string senha)
        {
            return !string.IsNullOrWhiteSpace(senha) &&
            senha.Length >= 6 &&
            senha.Any(char.IsUpper) &&
            senha.Any(char.IsLower) &&
            senha.Any(char.IsDigit) &&
            senha.Any(c => !char.IsLetterOrDigit(c));
        }
    }
}
