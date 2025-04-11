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
        private readonly IBuscarEnderecosCliente _buscarEnderecosCliente;

        public EnderecoController(IBuscarEnderecoApiExterna buscarEnderecoApiExterna, ICriarEndereco criarEndereco, IBuscarEnderecosCliente buscarEnderecosCliente)
        {
            _buscarEnderecoApiExterna = buscarEnderecoApiExterna;
            _criarEndereco = criarEndereco;
            _buscarEnderecosCliente = buscarEnderecosCliente;
        }

        [HttpGet("porCep/{cep}")]
        public async Task<IActionResult> Get(string cep)
        {
            var endereco = await _buscarEnderecoApiExterna.Executar(cep);

            return Ok(endereco);
        }

        [HttpPost("{clienteId}")]
        public async Task<IActionResult> CriarNovoEndereco(Guid clienteId, [FromBody]CriarEnderecoRequest request)
        {
            var endereco = await _criarEndereco.Executar(request, clienteId);

            return Created();
        }
        
        [HttpGet("{clienteId}")]
        public async Task<IActionResult> BuscarEnderecosCliente(Guid clienteId)
        {
            var enderecos = await _buscarEnderecosCliente.Executar(clienteId);

            return Ok(enderecos);
        }
    }
}
