using Umio.API.Application.CasosDeUso.Cupoms.Interfaces;
using Umio.API.Application.Contratos.Repositorios;
using Umio.API.Entities.Entidades;
using Umio.API.Entities.Exceptions;

namespace Umio.API.Application.CasosDeUso.Cupoms
{
    internal class BuscarCodigoCupom : IBuscarCodigoCupom
    {
        private readonly ICupomRepository _cupomRepository;
        public BuscarCodigoCupom(ICupomRepository cupomRepository)
        {
            _cupomRepository = cupomRepository;
        }
        public async Task<Cupom> Executar(string codigo, bool ativo)
        {
            var cupom = await _cupomRepository.BuscarPorCodigo(codigo, ativo);

            if(cupom == null)
                throw new ExcecaoElementoNaoEncontrado(nameof(cupom), codigo);

            return cupom;
        }
    }
}
