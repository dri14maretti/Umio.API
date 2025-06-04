namespace Umio.API.Entities.Exceptions
{
    public class ExcecaoParametroIncorreto : Exception
    {
        public ExcecaoParametroIncorreto(string parametro) : base($"O parametro '{parametro}' não encontrou resultado")
        {
        }

        public ExcecaoParametroIncorreto(string parametro, string mensagem) : base($"A parametro '{parametro}' é inválida. {mensagem}")
        {
        }

        public ExcecaoParametroIncorreto(string parametro, string mensagem, Exception innerException) : base($"A parametro '{parametro}' é inválida. {mensagem}", innerException)
        {
        }
    }
}
