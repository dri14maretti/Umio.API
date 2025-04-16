using Microsoft.AspNetCore.Mvc;
using Umio.API.Application.CasosDeUso.Usuarios.Interfaces;
using Umio.API.Controllers.Dtos.Requests;
using Umio.API.Controllers.DTOs.Requests;
using Umio.API.Controllers.DTOs.Responses;

namespace Umio.API.Controllers.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly ICriarCliente _criarUsuario;
        private readonly IAtualizarCliente _atualizarCliente;
        private readonly IDeletarCliente _deletarCliente;
        private readonly IListarCliente _listarClientes;
        public UsuarioController(ICriarCliente criarUsuario, IAtualizarCliente atualizarCliente, IDeletarCliente deletarCliente, IListarCliente listarClientes)
        {
            _listarClientes = listarClientes;
            _criarUsuario = criarUsuario;
            _atualizarCliente = atualizarCliente;
            _deletarCliente = deletarCliente;
        }

        [HttpPost("criar-usuario")]
        public async Task<IActionResult> CriarUsuario([FromBody] CriarClienteRequest usuario)
        {
            await _criarUsuario.Executar(usuario.Nome, usuario.Email, usuario.Telefone);

            return Ok(usuario);
        }

        [HttpPatch("atualizar-usuario/{id:guid}")]
        public async Task<IActionResult> AtualizarCliente(Guid id, [FromBody] AtualizarClienteRequest usuarioAtualizado)
        {
            try
            {
                var cliente = await _atualizarCliente.Executar(
                    id,
                    usuarioAtualizado.Nome,
                    usuarioAtualizado.Telefone
                );

                if (cliente == null) return NotFound("Cliente não encontrado.");

                return Ok(new
                {
                    Id = cliente.Id,
                    Nome = cliente.Nome,
                    Telefone = cliente.Telefone,
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("deletar-usuario/{id:guid}")]
        public async Task<IActionResult> DeletarCliente(Guid id)
        {
            if (id == Guid.Empty) return BadRequest("ID inválido.");

            var cliente = await _deletarCliente.Executar(id);
            if (cliente == null) return NotFound("Cliente não encontrado.");
            return NoContent();
        }

        [HttpGet("listar-clientes-filtrados")]
        public async Task<IActionResult> ListarClientes([FromQuery] string? nome = null, [FromQuery] string? email = null)
        {
            var clientes = await _listarClientes.Executar();
            var response = clientes.Select(c => new ClienteResponse(
                c.Id,
                c.Nome,
                c.Email,
                c.Telefone,
                c.Pontos
            ));

            return Ok(response);
        }
    }
}
