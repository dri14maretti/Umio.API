namespace Umio.API.Application.CasosDeUso.Clientes.Inputs
{
    public record CriarClienteOutput(
        Guid Id,
        string Nome,
        string Email,
        string Telefone
    );
}