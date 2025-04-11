using Microsoft.AspNetCore.Mvc;
using Umio.API.Application.CasosDeUso.Enderecos.Interfaces;
using Umio.API.Application.Contratos;

namespace Umio.API.Controllers.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnderecoController : ControllerBase
    {
        private readonly IBuscarEnderecoApiExterna _buscarEnderecoApiExterna;
        private readonly ICriarEndereco _criarEndereco;

        public EnderecoController(IBuscarEnderecoApiExterna buscarEnderecoApiExterna, ICriarEndereco criarEndereco)
        {
            _buscarEnderecoApiExterna = buscarEnderecoApiExterna;
            _criarEndereco = criarEndereco;
        }

        [HttpGet("{cep}")]
        public async Task<IActionResult> Get(string cep)
        {
            var endereco = await _buscarEnderecoApiExterna.Executar(cep);

            return Ok(endereco);
        }

        [HttpPost]
        public async Task<IActionResult> CriarNovoEndereco(CriarEnderecoRequest request)
        {
            var endereco = await _criarEndereco.Executar(request);

            return Created();
        }
    }
}
