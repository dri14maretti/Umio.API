using Microsoft.AspNetCore.Mvc;
using Umio.API.Application.CasosDeUso.Produtos.Interfaces;
using Umio.API.Controllers.Models;
using Umio.API.Entities.Entidades.Produtos;

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

            return Ok(ApiRetorno<IEnumerable<Produto>>.Sucesso(produtos));
        }
    }
}
