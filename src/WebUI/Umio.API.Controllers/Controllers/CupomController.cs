using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Umio.API.Application.CasosDeUso.Cupoms.Interfaces;
using Umio.API.Controllers.Models;
using Umio.API.Entities.Entidades;

namespace Umio.API.Controllers.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CupomController : ControllerBase
    {
        private readonly IBuscarCodigoCupom _buscarCodigoCupom;
        public CupomController(IBuscarCodigoCupom buscarCodigoCupom)
        {
            _buscarCodigoCupom = buscarCodigoCupom;
        }
        [HttpGet("{codigo}")]
        public async Task<IActionResult> BuscarCodigoCupom(string codigo, [FromQuery] bool ativo = true)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                return BadRequest("Código do cupom é obrigatório.");

            var cupom = await _buscarCodigoCupom.Executar(codigo, ativo);

            return Ok(ApiRetorno<Cupom>.Sucesso(cupom));

        }
    }
}
