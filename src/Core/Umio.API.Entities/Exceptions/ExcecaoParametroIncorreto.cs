namespace Umio.API.Entities.Exceptions
{
    public class ExcecaoParametroIncorreto : Exception
    {
        public ExcecaoParametroIncorreto(string parametro) : base($"O parametro '{parametro}' não encontrou resultado")
        {
        }

        public ExcecaoParametroIncorreto(string parametro, string mensagem) : base($"O parametro '{parametro}' é inválido. {mensagem}")
        {
        }

        public ExcecaoParametroIncorreto(string parametro, string mensagem, Exception innerException) : base($"O parametro '{parametro}' é inválido. {mensagem}", innerException)
        {
        }
    }
}
