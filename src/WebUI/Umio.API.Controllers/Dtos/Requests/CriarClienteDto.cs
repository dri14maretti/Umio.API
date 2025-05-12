namespace Umio.API.Controllers.DTOs.Requests
{
    public class CriarClienteRequest
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public Provedor Provedor { get; set; }
        public string Senha { get; set; }
    }

    public enum Provedor
    {
        Umio,
        Google,
        Apple
    }
}