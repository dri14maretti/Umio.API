using Microsoft.AspNetCore.Mvc;
using Umio.API.Application.CasosDeUso.Produtos.Interfaces;

namespace Umio.API.Controllers.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IListarProdutos _listarProdutos;
        public ProdutoController(IListarProdutos listarProdutos)
        {
            _listarProdutos = listarProdutos;
        }
        [HttpGet]
        public async Task<IActionResult> ListarProdutos()
        {
            var produtos = await _listarProdutos.Executar();

            return Ok(produtos);
        }
    }
}
