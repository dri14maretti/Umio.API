
namespace Umio.API.Controllers.DTOs.Responses
{
    public class CriarClienteResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string? FotoUrl { get; set; }
    }
}