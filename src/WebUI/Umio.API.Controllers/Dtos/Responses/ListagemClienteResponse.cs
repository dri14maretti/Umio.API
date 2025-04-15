namespace Umio.API.Controllers.DTOs.Responses;

public record ClienteResponse(
    Guid Id,
    string Nome,
    string Email,
    string Telefone,
    int Pontos,
    string FotoUrl
);