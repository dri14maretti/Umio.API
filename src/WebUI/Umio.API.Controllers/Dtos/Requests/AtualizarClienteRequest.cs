namespace Umio.API.Controllers.Dtos.Requests
{
    public record AtualizarClienteRequest(
        string? Nome,
        string? Telefone,
        string? FotoUrl
    );
}