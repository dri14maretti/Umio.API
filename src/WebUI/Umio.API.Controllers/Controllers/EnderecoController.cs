using Microsoft.AspNetCore.Mvc;
using Umio.API.Application.CasosDeUso.Enderecos.Interfaces;
using Umio.API.Application.Contratos;
using Umio.API.Controllers.Models;
using Umio.API.Entities.Entidades;

namespace Umio.API.Controllers.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnderecoController : ControllerBase
    {
        private readonly IBuscarEnderecoApiExterna _buscarEnderecoApiExterna;
        private readonly ICriarEndereco _criarEndereco;
        private readonly IBuscarEnderecosCliente _buscarEnderecosCliente;
        private readonly IExcluirEndereco _excluirEndereco;

        public EnderecoController(IBuscarEnderecoApiExterna buscarEnderecoApiExterna, ICriarEndereco criarEndereco, IBuscarEnderecosCliente buscarEnderecosCliente, IExcluirEndereco excluirEndereco)
        {
            _buscarEnderecoApiExterna = buscarEnderecoApiExterna;
            _criarEndereco = criarEndereco;
            _buscarEnderecosCliente = buscarEnderecosCliente;
            _excluirEndereco = excluirEndereco;
        }

        [HttpGet("porCep/{cep}")]
        public async Task<IActionResult> Get(string cep)
        {
            var endereco = await _buscarEnderecoApiExterna.Executar(cep);

            return Ok(ApiRetorno<Endereco>.Sucesso(endereco));
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

            return Ok(ApiRetorno<IEnumerable<Endereco>>.Sucesso(enderecos));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirEndereco(Guid id)
        {
            var endereco = await _excluirEndereco.Executar(id);

            return NoContent();
        }
    }
}
