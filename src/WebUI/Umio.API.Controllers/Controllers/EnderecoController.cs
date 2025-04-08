using Microsoft.AspNetCore.Mvc;
using Umio.API.Application.CasosDeUso.Enderecos.Interfaces;

namespace Umio.API.Controllers.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnderecoController : ControllerBase
    {
        private readonly IBuscarEnderecoApiExterna _buscarEnderecoApiExterna;

        public EnderecoController(IBuscarEnderecoApiExterna buscarEnderecoApiExterna)
        {
            _buscarEnderecoApiExterna = buscarEnderecoApiExterna;
        }

        [HttpGet("{cep}")]
        public async Task<IActionResult> Get(string cep)
        {
            var endereco = await _buscarEnderecoApiExterna.Executar(cep);

            return Ok(endereco);
        }
    }
}
