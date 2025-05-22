using Microsoft.AspNetCore.Mvc;
using Umio.API.Application.CasosDeUso.Usuarios.Interfaces;
using Umio.API.Controllers.Dtos.Requests;
using Umio.API.Controllers.Models;

namespace Umio.API.Controllers.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginUsuario loginUsuario;
        public LoginController(ILoginUsuario loginUsuario)
        {
            this.loginUsuario = loginUsuario;
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Senha))
                return BadRequest("Email e senha são obrigatórios.");

            var token = await loginUsuario.GerarToken(request.Email, request.Senha);

            return Ok(ApiRetorno<string>.Sucesso(token));
        }
    }
}
