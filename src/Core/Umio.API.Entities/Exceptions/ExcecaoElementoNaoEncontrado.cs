namespace Umio.API.Entities.Exceptions
{
    public class ExcecaoElementoNaoEncontrado : Exception
    {
        public ExcecaoElementoNaoEncontrado(string nomeElemento, string identificador)
            : base($"O elemento '{nomeElemento}' com o identificador '{identificador}' não foi encontrado.")
        {
        }

        public ExcecaoElementoNaoEncontrado(string nomeElemento)
            : base($"O elemento '{nomeElemento}' não foi encontrado.")
        {
        }
    }
}
