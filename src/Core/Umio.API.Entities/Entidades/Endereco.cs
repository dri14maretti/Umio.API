namespace Umio.API.Entities.Entidades
{
    public class Endereco
    {
        private Endereco(string cep, string rua, string bairro, string cidade, string uf, int numero, Guid usuarioId)
        {
            Cep = cep;
            Rua = rua;
            Bairro = bairro;
            Cidade = cidade;
            UF = uf;
            Numero = numero;
            UsuarioId = usuarioId;
        }

        private Endereco(string cep, string rua, string bairro, string cidade, string uf)
        {
            Cep = cep;
            Rua = rua;
            Bairro = bairro;
            Cidade = cidade;
            UF = uf;
        }

        public Guid? Id { get; private set; }
        public string Cep { get; private set; }
        public string Rua { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string UF { get; private set; }
        public int Numero { get; private set; }
        public Guid UsuarioId { get; private set; }


        public static Endereco CriarEnderecoSemNumero(string cep, string rua, string bairro, string cidade, string uf)
        {
            return new Endereco(cep, rua, bairro, cidade, uf);
        }

        public static Endereco CriarNovoEndereco(string cep, string rua, string bairro, string cidade, string uf, int numero, Guid usuarioId)
        {
            return new Endereco(cep, rua, bairro, cidade, uf, numero, usuarioId);
        }
    }
}
