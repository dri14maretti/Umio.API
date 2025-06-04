using Umio.API.Entities.Entidades;

namespace Umio.API.TestData.Entidades
{
    public static class DadosEndereco
    {
        public static string Cep => "12345678";
        public static string Rua => "Rua Exemplo";
        public static string Bairro => "Bairro Exemplo";
        public static string Cidade => "Cidade Exemplo";
        public static string UF => "SP";
        public static int Numero => 123;
        public static string Complemento => "Apto 101";
        public static Guid ClienteId => Guid.NewGuid();

        public static Endereco EnderecoValido => Endereco.CriarNovoEndereco(Cep, Rua, Bairro, Cidade, UF, Numero, ClienteId, Complemento);

        public static Endereco EnderecoSemNumero => Endereco.CriarEnderecoSemNumero(Cep, Rua, Bairro, Cidade, UF);
    }
}
