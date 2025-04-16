using System.Text.RegularExpressions;
using Umio.API.Entities.Exceptions;

namespace Umio.API.Entities.Entidades
{
    public class Cliente
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Telefone { get; private set; }
        public int Pontos { get; private set; }
        public List<Endereco> Enderecos { get; private set; } = [];
        public List<Pedido> Pedidos { get; private set; } = [];


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
        public void AtualizarCliente(string nome, string telefone)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ExcecaoPropriedadeInvalida(nameof(nome));

            Nome = nome;
            Telefone = ValidarTelefone(telefone);
        }
    }
}
