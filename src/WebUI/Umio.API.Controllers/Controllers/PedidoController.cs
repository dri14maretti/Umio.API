using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Umio.API.Application.CasosDeUso.Pedidos.Interfaces;
using Umio.API.Application.CasosDeUso.Pedidos.Model;

namespace Umio.API.Controllers.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class PedidoController : ControllerBase
    {
        private readonly IListarPedidosFiltro _listarPedidosFiltro;
        private readonly IRealizarPedido _realizarPedido;

        public PedidoController(IListarPedidosFiltro listarPedidosFiltro, IRealizarPedido realizarPedido)
        {
            _listarPedidosFiltro = listarPedidosFiltro;
            _realizarPedido = realizarPedido;
        }

        [HttpGet]
        public IActionResult ListarPedidos([FromQuery] Guid? usuarioId, [FromQuery] DateTime? data)
        {
            // Aqui você chamaria o caso de uso para listar pedidos
            return Ok("Lista de pedidos (ainda não implementado)");
        }

        [HttpPost]
        public async Task<IActionResult>CriarPedido(PedidoModel pedidoModel)
        {
            var claims = User.Claims.ToList();
            var usuarioId = claims.Where(x => x.Type == "aud").First().Value;

            var pedido = await _realizarPedido.Executar(pedidoModel, Guid.Parse(usuarioId));
            return Created("", "Pedido criado (ainda não implementado)");
        }

        // Outros métodos para atualizar, excluir, etc. podem ser adicionados aqui
    }
}
