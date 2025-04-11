namespace Umio.API.Entities.Exceptions
{
    public class ExcecaoPropriedadeInvalida : Exception
    {
        public ExcecaoPropriedadeInvalida(string propriedade, string mensagem) : base($"A propriedade '{propriedade}' é inválida. {mensagem}")
        {
        }

        public ExcecaoPropriedadeInvalida(string propriedade, string mensagem, Exception innerException) : base($"A propriedade '{propriedade}' é inválida. {mensagem}", innerException)
        {
        }
    }
}
