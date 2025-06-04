using Microsoft.AspNetCore.Mvc;
using Umio.API.Application.CasosDeUso.CategoriasProduto.Interfaces;
using Umio.API.Controllers.Models;
using Umio.API.Entities.Entidades;

namespace Umio.API.Controllers.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriaProdutoController : ControllerBase
    {

        private readonly IListarCategoriasProduto _listarCategorias;
        public CategoriaProdutoController(IListarCategoriasProduto listarCategorias)
        {
            _listarCategorias = listarCategorias;
        }

        [HttpGet]
        public async Task<IActionResult> ListarCategorias() => 
            Ok(ApiRetorno<IEnumerable<CategoriaProduto>>.Sucesso(
                await _listarCategorias.Executar()));

    }
}
