using Umio.API.Application.CasosDeUso.Enderecos.Interfaces;
using Umio.API.Application.Contratos.Servicos;
using Umio.API.Entities.Entidades;

namespace Umio.API.Application.CasosDeUso.Enderecos
{
    internal class BuscarEnderecoApiExterna : IBuscarEnderecoApiExterna
    {
        private readonly ICepService _cepService;

        public BuscarEnderecoApiExterna(ICepService cepService)
        {
            _cepService = cepService;
        }

        public async Task<Endereco> Executar(string cep)
        {
            var endereco = await _cepService.BuscarEnderecoPorCep(cep);

            return endereco;
        }
    }
}
