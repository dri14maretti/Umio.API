using Microsoft.AspNetCore.Mvc;
using Umio.API.Application.CasosDeUso.Usuarios.Interfaces;

namespace Umio.API.Controllers.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        public ICriarCliente _criarUsuario;
        public UsuarioController(ICriarCliente criarUsuario)
        {
            _criarUsuario = criarUsuario;
        }

        [HttpPost]
        public async Task<IActionResult> CriarUsuario([FromBody] object usuario)
        {
            //await _criarUsuario.Executar(usuario);

            return Ok();
        }
    }
}
