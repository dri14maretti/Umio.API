using Microsoft.AspNetCore.Mvc;
using Umio.API.Application.CasosDeUso.Clientes.Inputs;
using Umio.API.Application.CasosDeUso.Clientes.Interfaces;
using Umio.API.Controllers.Dtos.Requests;
using Umio.API.Controllers.DTOs.Requests;
using Umio.API.Controllers.Models;
using Umio.API.Entities.Entidades;

namespace Umio.API.Controllers.Controllers
{
    [ApiController]
    [Route("api/clientes")]
    public class ClienteController : ControllerBase
    {
        private readonly ICriarCliente _criarCliente;
        private readonly IAtualizarCliente _atualizarCliente;
        private readonly IDeletarCliente _deletarCliente;
        private readonly IListarCliente _listarClientes;
        public ClienteController(
            ICriarCliente criarCliente,
            IAtualizarCliente atualizarCliente,
            IDeletarCliente deletarCliente,
            IListarCliente listarClientes)
        {
            _criarCliente = criarCliente;
            _atualizarCliente = atualizarCliente;
            _deletarCliente = deletarCliente;
            _listarClientes = listarClientes;
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarClienteRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Nome)
                || string.IsNullOrWhiteSpace(request.Email)
                || string.IsNullOrWhiteSpace(request.Senha)
                || string.IsNullOrWhiteSpace(request.Telefone)
                || string.IsNullOrWhiteSpace(request.Provedor.ToString()))
                return BadRequest("Nome, e-mail, telefone, provedor e senha são obrigatórios.");

            var input = new CriarClienteInput(
                request.Nome,
                request.Email,
                request.Telefone,
                request.Senha,
                request.Provedor
            );

            var result = await _criarCliente.Executar(input);
            return Ok(ApiRetorno<CriarClienteOutput>.Sucesso(result));
        }

        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] AtualizarClienteRequest usuarioAtualizado)
        {
            var cliente = await _atualizarCliente.Executar(
                id,
                usuarioAtualizado.Nome,
                usuarioAtualizado.Telefone,
                usuarioAtualizado.Email,
                usuarioAtualizado.Pontos
            );
            return cliente is null ? NotFound() : Ok(ApiRetorno<Cliente>.Sucesso(cliente));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            if (id == Guid.Empty) return BadRequest("ID inválido.");

            var cliente = await _deletarCliente.Executar(id);
            return cliente ? NoContent() : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> ListarComFiltros([FromQuery] string? nome = null, [FromQuery] string? email = null, [FromQuery] Guid? id = null)
        {
            var clientes = await _listarClientes.Executar(nome, email, id);
            return Ok(ApiRetorno<IEnumerable<Cliente>>.Sucesso(clientes));
        }
    }
}
