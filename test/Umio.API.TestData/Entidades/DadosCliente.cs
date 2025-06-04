using Umio.API.Entities.Entidades;

namespace Umio.API.TestData.Entidades
{
    public class DadosCliente
    {
        public static string Nome => "Adriano";
        public static string Email => "a@gmail.com";
        public static string Telefone => "11 12345 6789";
        public static Guid Id => Guid.NewGuid();
        public Cliente ClienteValido = Cliente.CriarNovoCliente("Adriano", "a@gmail.com", "11 12345 6789");
    }
}
