using System.Text.RegularExpressions;
namespace Umio.API.Entities.Entidades
{
    public partial class Cliente
    {
        public Guid Id { get; private set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Telefone { get; set; }
        public int Pontos { get; private set; }
        public string FotoUrl { get; private set; } = "";
        public List<Endereco> Enderecos { get; private set; } = [];
        public List<Pedido> Pedidos { get; private set; } = [];

        public Cliente(
            string nome,
            string email,
            string telefone,
            string? fotoUrl)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Email = ValidarEmail(email);
            Telefone = ValidarTelefone(telefone);
            FotoUrl = "";
            Pontos = 0;
            Enderecos = [];
            Pedidos = [];
        }

        private static string ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email não pode ser vazio");

            if (!MyRegex().IsMatch(email))
                throw new ArgumentException("Email inválido");

            return email;
        }

        private static string ValidarTelefone(string telefone)
        {
            var apenasNumeros = new string(telefone.Where(char.IsDigit).ToArray());

            if (apenasNumeros.Length < 11)
                throw new ArgumentException("Telefone inválido (DDD + número)");

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

        [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
        private static partial Regex MyRegex();
    }
}
