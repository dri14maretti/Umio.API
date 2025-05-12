namespace Umio.API.Application.CasosDeUso.Clientes.Inputs
{
    public record CriarClienteInput(
        string Nome,
        string Email,
        string Telefone,
        string Senha,
        string Provedor
    );
}
